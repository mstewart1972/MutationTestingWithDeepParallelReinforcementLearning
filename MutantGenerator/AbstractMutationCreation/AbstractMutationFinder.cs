using Mono.Cecil;
using Mono.Cecil.Cil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.Mutations;
using MutantGeneration.MutationSteps;
using System.Collections.Generic;

namespace MutantGeneration.AbstractMutationCreation
{
    public static class AbstractMutationFinder
    {
        private static MutationFamily amc = new MutationFamily { Name = "AMC" };
        private static MutationFamily ror = new MutationFamily { Name = "ROR" };
        private static MutationFamily aor = new MutationFamily { Name = "AOR" };
        private static MutationFamily lor = new MutationFamily { Name = "LOR" };
        private static MutationFamily uoi = new MutationFamily { Name = "uoi" };
        private const string OP_INEQUALITY_METHOD_NAME = "op_Inequality";
        private const string OP_EQUALITY_METHOD_NAME = "op_Equality";

        private static IMutationPurpose greaterThanToLessThanOrEqual = new MutationPurpose { Description = "Replace greater than with less than or equal (>) <-> (<=)" };
        private static IMutationPurpose lessThanToGreaterThanOrEqual = new MutationPurpose { Description = "Replace less than with greater than or equal (<) <-> (>=)" };
        private static IMutationPurpose equalToNotEqual = new MutationPurpose { Description = "Replace equals with not equal and not equal with equals (==) <-> (!=)" };

        private static IMutationPurpose addToSub = new MutationPurpose { Description = "Replace addition with subtraction (+) -> (-)" };
        private static IMutationPurpose addToMul = new MutationPurpose { Description = "Replace addition with multiplication (+) -> (*)" };
        private static IMutationPurpose subToAdd = new MutationPurpose { Description = "Replace subtraction with addition (-) -> (+)" };
        private static IMutationPurpose mulToDiv = new MutationPurpose { Description = "Replace multiplication with division (*) -> (/)" };
        private static IMutationPurpose divToMul = new MutationPurpose { Description = "Replace division with multiplication (/) -> (*)" };
        private static IMutationPurpose remToMul = new MutationPurpose { Description = "Replace modulus with multiplication (%) -> (*)" };

        private static IMutationPurpose insertNot = new MutationPurpose { Description = "Insert not in front of boolean expressions () <-> (!)" };

        private static IMutationPurpose publicToPrivate = new MutationPurpose { Description = "Replace public with private (public) -> (private)" };
        private static IMutationPurpose protectedToPrivate = new MutationPurpose { Description = "Replace protected with private (protected) -> (private)" };
        private static IMutationPurpose publicToProtected = new MutationPurpose { Description = "Replace public with protected (public) -> (protected)" };
        private static IMutationPurpose privateToProtected = new MutationPurpose { Description = "Replace private with protected (private) -> (protected)" };
        private static IMutationPurpose privateToPublic = new MutationPurpose { Description = "Replace private with public (private) -> (public)" };
        private static IMutationPurpose protectedToPublic = new MutationPurpose { Description = "Replace protected with public (protected) -> (public)" };

        private static IEnumerable<ReplacementPair> replacements = new List<ReplacementPair>
        {
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Ble,Purpose=greaterThanToLessThanOrEqual},
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Ble_S,Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Ble_Un,Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Ble_Un_S,Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bge,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bge_S,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bge_Un,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bge_Un_S,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Beq,NewCode=OpCodes.Bne_Un, Purpose=equalToNotEqual },
            new ReplacementPair{OldCode=OpCodes.Beq_S,NewCode=OpCodes.Bne_Un_S, Purpose=equalToNotEqual },

            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Sub, Purpose=addToSub },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Mul, Purpose=addToMul },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Add, Purpose=subToAdd },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Div, Purpose=mulToDiv },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Mul, Purpose=divToMul },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Mul, Purpose=remToMul },

        };

        private static IEnumerable<OpCode> nottableCodes = new List<OpCode>
        {
            OpCodes.Cgt,
            OpCodes.Cgt_Un,
            OpCodes.Clt,
            OpCodes.Clt_Un,
            OpCodes.Ceq
        };

        private static IEnumerable<ReplacementPair> operandReplacements = new List<ReplacementPair>
        {
            new ReplacementPair{OldCode=OpCodes.Ldc_I4_0,NewCode=OpCodes.Ldc_I4_1 ,Purpose=insertNot },
            new ReplacementPair{OldCode=OpCodes.Ldc_I4_1,NewCode=OpCodes.Ldc_I4_0 ,Purpose=insertNot },
        };

        private static IEnumerable<ReplacementPair> conditionalJumpReplacements = new List<ReplacementPair>
        {
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Ble, Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Ble_S, Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Ble_Un, Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Ble_Un_S, Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bge, Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bge_S, Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bge_Un, Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bge_Un_S, Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Beq,NewCode=OpCodes.Bne_Un,Purpose=equalToNotEqual },
            new ReplacementPair{OldCode=OpCodes.Beq_S,NewCode=OpCodes.Bne_Un_S,Purpose=equalToNotEqual },
            new ReplacementPair{OldCode=OpCodes.Brtrue,NewCode=OpCodes.Brfalse, Purpose=insertNot },
            new ReplacementPair{OldCode=OpCodes.Brtrue_S,NewCode=OpCodes.Brfalse_S, Purpose=insertNot },
        };

        private static IEnumerable<MethodReplacementPair> methodReplacements = new List<MethodReplacementPair>
        {
            new MethodReplacementPair{ oldMethodName=OP_INEQUALITY_METHOD_NAME, newMethodName=OP_EQUALITY_METHOD_NAME, Purpose=equalToNotEqual},
            new MethodReplacementPair{ oldMethodName=OP_EQUALITY_METHOD_NAME, newMethodName=OP_INEQUALITY_METHOD_NAME, Purpose=equalToNotEqual}
        };

        private static IEnumerable<FieldAttributeReplacementPair> fieldAttributeReplacements = new List<FieldAttributeReplacementPair>
        {
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Public, NewAttribute = FieldAttributes.Private, Purpose=publicToPrivate},
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Public, NewAttribute = FieldAttributes.Family, Purpose=publicToProtected},
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Private, NewAttribute = FieldAttributes.Public, Purpose=privateToPublic},
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Private, NewAttribute = FieldAttributes.Family, Purpose=privateToProtected},
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Family, NewAttribute = FieldAttributes.Public, Purpose=protectedToPublic},
            new FieldAttributeReplacementPair { OldAttribute = FieldAttributes.Family, NewAttribute = FieldAttributes.Private, Purpose=protectedToPrivate},
        };

        public static IEnumerable<IAbstractMutation<InstructionContext>> GetAllAbstractInstructionMutations()
        {
            var mutations = new List<IAbstractMutation<InstructionContext>>();
            foreach (var replacement in replacements)
            {
                var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose=replacement.Purpose};
                var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                mutations.Add(abstractMutationF);
                mutations.Add(abstractMutationB);
            }
            foreach(OpCode code in nottableCodes)
            {
                var forwardSteps = new InsertFollowingNot();
                var abstractMutationF = new AbstractMutation<InstructionContext> { Family = ror, CodeFilter = i => i.Instruction.OpCode.Equals(code), MutationSteps = forwardSteps, Purpose=insertNot };
                mutations.Add(abstractMutationF);
            }
            foreach(var jumpReplacement in conditionalJumpReplacements)
            {
                foreach(var operandReplacement in operandReplacements)
                {
                    var forwardSteps = new ReplaceOperationAndOperandOpCode(jumpReplacement.NewCode, jumpReplacement.OldCode, operandReplacement.NewCode, operandReplacement.OldCode);
                    var backwardSteps = new ReplaceOperationAndOperandOpCode(jumpReplacement.OldCode, jumpReplacement.NewCode, operandReplacement.OldCode, operandReplacement.NewCode);
                    var abstractMutationF = new AbstractMutation<InstructionContext> { Family = ror, CodeFilter = i => i.Instruction.OpCode.Equals(jumpReplacement.OldCode)&&
                    ((Instruction)i.Instruction.Operand).OpCode.Equals(operandReplacement.OldCode)                    
                    , MutationSteps = forwardSteps ,
                        Purpose = jumpReplacement.Purpose
                    };
                    var abstractMutationB = new AbstractMutation<InstructionContext> { Family = ror, CodeFilter = i => i.Instruction.OpCode.Equals(jumpReplacement.NewCode)&&
                    ((Instruction)i.Instruction.Operand).OpCode.Equals(operandReplacement.NewCode)
                    , MutationSteps = backwardSteps, Purpose=jumpReplacement.Purpose };
                    mutations.Add(abstractMutationF);
                    mutations.Add(abstractMutationB);
                }
            }
            foreach(var replacement in methodReplacements)
            {
                var forwardSteps = new ReplaceMethodCall(replacement.newMethodName, replacement.oldMethodName);
                var abstractMutationF = new AbstractMutation<InstructionContext>
                {
                    Family = uoi,
                    CodeFilter = i => (i.Instruction.Operand is MethodReference) &&
                    (i.Instruction.Operand as MethodReference).Name.Equals(replacement.oldMethodName),                    
                    MutationSteps = forwardSteps,
                    Purpose = replacement.Purpose
                };
                mutations.Add(abstractMutationF);

            }


            return mutations;
        }

        public static IEnumerable<IAbstractMutation<FieldContext>> GetAllAbstractFieldMutations()
        {
            var mutations = new List<IAbstractMutation<FieldContext>>();
            foreach(var replacement in fieldAttributeReplacements)
            {
                var steps = new ReplaceFieldAttribute(replacement.NewAttribute, replacement.OldAttribute);
                var abstractMutation = new AbstractMutation<FieldContext>
                {
                    Family = amc,
                    CodeFilter = i => i.Field.Attributes.HasFlag(replacement.OldAttribute),
                    MutationSteps = steps,
                    Purpose = replacement.Purpose
                };
                mutations.Add(abstractMutation);
            }
            return mutations;
        }
    }
}
