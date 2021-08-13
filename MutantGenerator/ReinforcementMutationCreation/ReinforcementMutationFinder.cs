using Mono.Cecil;
using Mono.Cecil.Cil;
using MutantCommon;
using MutantGeneration.CodeContexts;
using MutantGeneration.Mutations;
using MutantGeneration.MutationSteps;
using System.Collections.Generic;

// MAS 20210118
namespace MutantGeneration.ReinforcementMutationCreation
{
    public static class ReinforcementMutationFinder
    {
        private static MutationFamily amc = new MutationFamily { Name = "AMC" };
        private static MutationFamily ror = new MutationFamily { Name = "ROR" };
        private static MutationFamily aor = new MutationFamily { Name = "AOR" };
        // MAS 20210318
        private static MutationFamily aorb = new MutationFamily { Name = "AORB" };
        private static MutationFamily lor = new MutationFamily { Name = "LOR" };
        private static MutationFamily uoi = new MutationFamily { Name = "uoi" };
        private const string OP_INEQUALITY_METHOD_NAME = "op_Inequality";
        private const string OP_EQUALITY_METHOD_NAME = "op_Equality";

        private static IMutationPurpose greaterThanToLessThanOrEqual = new MutationPurpose { Description = "Replace greater than with less than or equal (>) <-> (<=)" };
        private static IMutationPurpose lessThanToGreaterThanOrEqual = new MutationPurpose { Description = "Replace less than with greater than or equal (<) <-> (>=)" };
        private static IMutationPurpose equalToNotEqual = new MutationPurpose { Description = "Replace equals with not equal and not equal with equals (==) <-> (!=)" };

        // MAS 20210316
        private static IMutationPurpose greaterThanToLessThan = new MutationPurpose { Description = "Replace greater than with less than (>) <-> (<)" };
        private static IMutationPurpose greaterThanToEqual = new MutationPurpose { Description = "Replace greater than with equal (>) <-> (==)" };
        private static IMutationPurpose greaterThanToNotEqual = new MutationPurpose { Description = "Replace greater than with not equal (>) <-> (!=)" };
        private static IMutationPurpose lessThanToGreaterThan = new MutationPurpose { Description = "Replace less than with greater than (<) <-> (>)" };
        private static IMutationPurpose lessThanToEqual = new MutationPurpose { Description = "Replace less than with equal (<) <-> (==)" };
        private static IMutationPurpose lessThanToNotEqual = new MutationPurpose { Description = "Replace less than with not equal (<) <-> (!=)" };
        private static IMutationPurpose notEqualToEqual = new MutationPurpose { Description = "Replace not equal with equal (!=) <-> (==)" };
        //

        private static IMutationPurpose addToSub = new MutationPurpose { Description = "Replace addition with subtraction (+) -> (-)" };
        private static IMutationPurpose addToMul = new MutationPurpose { Description = "Replace addition with multiplication (+) -> (*)" };
        private static IMutationPurpose addToDiv = new MutationPurpose { Description = "Replace addition with division (+) -> (/)" };
        private static IMutationPurpose addToRem = new MutationPurpose { Description = "Replace addition with modulus (+) -> (%)" };

        private static IMutationPurpose subToAdd = new MutationPurpose { Description = "Replace subtraction with addition (-) -> (+)" };
        private static IMutationPurpose subToMul = new MutationPurpose { Description = "Replace subtraction with multiplication (-) -> (*)" };
        private static IMutationPurpose subToDiv = new MutationPurpose { Description = "Replace subtraction with division (-) -> (/)" };
        private static IMutationPurpose subToRem = new MutationPurpose { Description = "Replace subtraction with modulus (-) -> (%)" };


        private static IMutationPurpose mulToSub = new MutationPurpose { Description = "Replace multiplication with subtraction (*) -> (-)" };
        private static IMutationPurpose mulToAdd = new MutationPurpose { Description = "Replace multiplication with addition (*) -> (+)" };
        private static IMutationPurpose mulToDiv = new MutationPurpose { Description = "Replace multiplication with division (*) -> (/)" };
        private static IMutationPurpose mulToRem = new MutationPurpose { Description = "Replace multiplication with modulus (*) -> (%)" };

        private static IMutationPurpose divToMul = new MutationPurpose { Description = "Replace division with multiplication (/) -> (*)" };
        private static IMutationPurpose divToSub = new MutationPurpose { Description = "Replace division with subtraction (/) -> (-)" };
        private static IMutationPurpose divToAdd = new MutationPurpose { Description = "Replace division with addition (/) -> (+)" };
        private static IMutationPurpose divToRem = new MutationPurpose { Description = "Replace division with modulus (/) -> (%)" };

        private static IMutationPurpose remToMul = new MutationPurpose { Description = "Replace modulus with multiplication (%) -> (*)" };
        private static IMutationPurpose remToSub = new MutationPurpose { Description = "Replace modulus with subtraction (%) -> (-)" };
        private static IMutationPurpose remToAdd = new MutationPurpose { Description = "Replace modulus with addition (%) -> (+)" };
        private static IMutationPurpose remToDiv = new MutationPurpose { Description = "Replace modulus with division (%) -> (/)" };


        private static IMutationPurpose insertNot = new MutationPurpose { Description = "Insert not in front of boolean expressions () <-> (!)" };

        private static IMutationPurpose publicToPrivate = new MutationPurpose { Description = "Replace public with private (public) -> (private)" };
        private static IMutationPurpose protectedToPrivate = new MutationPurpose { Description = "Replace protected with private (protected) -> (private)" };
        private static IMutationPurpose publicToProtected = new MutationPurpose { Description = "Replace public with protected (public) -> (protected)" };
        private static IMutationPurpose privateToProtected = new MutationPurpose { Description = "Replace private with protected (private) -> (protected)" };
        private static IMutationPurpose privateToPublic = new MutationPurpose { Description = "Replace private with public (private) -> (public)" };
        private static IMutationPurpose protectedToPublic = new MutationPurpose { Description = "Replace protected with public (protected) -> (public)" };

        private static IEnumerable<ReplacementPair> replacements = new List<ReplacementPair>
        {
            // Original Combinations - pre-defined, static usage
            //new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Ble,Purpose=greaterThanToLessThanOrEqual},
            //new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Ble_S,Purpose=greaterThanToLessThanOrEqual },
            //new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Ble_Un,Purpose=greaterThanToLessThanOrEqual },
            //new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Ble_Un_S,Purpose=greaterThanToLessThanOrEqual },

            //new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bge,Purpose=lessThanToGreaterThanOrEqual },
            //new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bge_S,Purpose=lessThanToGreaterThanOrEqual },
            //new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bge_Un,Purpose=lessThanToGreaterThanOrEqual },
            //new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bge_Un_S,Purpose=lessThanToGreaterThanOrEqual },

            //new ReplacementPair{OldCode=OpCodes.Beq,NewCode=OpCodes.Bne_Un, Purpose=equalToNotEqual },
            //new ReplacementPair{OldCode=OpCodes.Beq_S,NewCode=OpCodes.Bne_Un_S, Purpose=equalToNotEqual },

            //new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Sub, Purpose=addToSub },
            //new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Add, Purpose=subToAdd },
            //new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Div, Purpose=mulToDiv },
            //new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Mul, Purpose=divToMul },
            //new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Mul, Purpose=remToMul },


            // All Combinations - reinforcement learning, dynamic usage
            // greater than (Branch to target if greater than.)
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Ble,Purpose=greaterThanToLessThanOrEqual},
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Blt,Purpose=greaterThanToLessThan},
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Beq,Purpose=greaterThanToEqual},
            new ReplacementPair{OldCode=OpCodes.Bgt,NewCode=OpCodes.Bne_Un,Purpose=greaterThanToNotEqual},

            // greater than (Branch to target if greater than, short form.)
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Ble_S,Purpose=greaterThanToLessThanOrEqual},
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Blt_S,Purpose=greaterThanToLessThan},
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Beq_S,Purpose=greaterThanToEqual},
            new ReplacementPair{OldCode=OpCodes.Bgt_S,NewCode=OpCodes.Bne_Un_S,Purpose=greaterThanToNotEqual},

            // greater than (Branch to target if greater than (unsigned or unordered).)
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Ble_Un,Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Blt_Un,Purpose=greaterThanToLessThan },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Beq,Purpose=greaterThanToEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un,NewCode=OpCodes.Bne_Un,Purpose=greaterThanToNotEqual },

            // greater than (Branch to target if greater than (unsigned or unordered), short form.Branch to target if greater than.)
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Ble_Un_S,Purpose=greaterThanToLessThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Blt_Un_S,Purpose=greaterThanToLessThan },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Beq_S,Purpose=greaterThanToEqual },
            new ReplacementPair{OldCode=OpCodes.Bgt_Un_S,NewCode=OpCodes.Bne_Un_S,Purpose=greaterThanToNotEqual },

            // less than (Branch to target if greater than.)
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bge,Purpose=lessThanToGreaterThanOrEqual},
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bgt,Purpose=lessThanToGreaterThan},
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Beq,Purpose=lessThanToEqual},
            new ReplacementPair{OldCode=OpCodes.Blt,NewCode=OpCodes.Bne_Un,Purpose=lessThanToNotEqual},

            // less than (Branch to target if greater than, short form.)
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bge_S,Purpose=lessThanToGreaterThanOrEqual},
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bgt_S,Purpose=lessThanToGreaterThan},
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Beq_S,Purpose=lessThanToEqual},
            new ReplacementPair{OldCode=OpCodes.Blt_S,NewCode=OpCodes.Bne_Un_S,Purpose=lessThanToNotEqual},

            // less than (Branch to target if greater than (unsigned or unordered).)
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bge_Un,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bgt_Un,Purpose=lessThanToGreaterThan },
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Beq,Purpose=lessThanToEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un,NewCode=OpCodes.Bne_Un,Purpose=lessThanToNotEqual },

            // less than (Branch to target if greater than (unsigned or unordered), short form.Branch to target if greater than.)
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bge_Un_S,Purpose=lessThanToGreaterThanOrEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bgt_Un_S,Purpose=lessThanToGreaterThan },
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Beq_S,Purpose=lessThanToEqual },
            new ReplacementPair{OldCode=OpCodes.Blt_Un_S,NewCode=OpCodes.Bne_Un_S,Purpose=lessThanToNotEqual },

            // equal
            new ReplacementPair{OldCode=OpCodes.Beq,NewCode=OpCodes.Bne_Un, Purpose=equalToNotEqual },
            new ReplacementPair{OldCode=OpCodes.Beq_S,NewCode=OpCodes.Bne_Un_S, Purpose=equalToNotEqual },

            // not equal
            new ReplacementPair{OldCode=OpCodes.Bne_Un,NewCode=OpCodes.Beq, Purpose=notEqualToEqual },
            new ReplacementPair{OldCode=OpCodes.Bne_Un_S,NewCode=OpCodes.Beq_S, Purpose=notEqualToEqual },

            // addition
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Mul, Purpose=addToMul },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Sub, Purpose=addToSub },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Div, Purpose=addToDiv },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Rem, Purpose=addToRem },

            // subtraction
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Mul, Purpose=subToMul },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Add, Purpose=subToAdd },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Div, Purpose=subToDiv },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Rem, Purpose=subToRem },

            // multiplication
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Add, Purpose=mulToAdd },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Sub, Purpose=mulToSub },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Div, Purpose=mulToDiv },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Rem, Purpose=mulToRem },

            // division
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Add, Purpose=divToAdd },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Sub, Purpose=divToSub },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Mul, Purpose=divToMul },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Rem, Purpose=divToRem },

            // modulo
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Add, Purpose=remToAdd },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Sub, Purpose=remToSub },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Div, Purpose=remToDiv },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Mul, Purpose=remToMul },
        };


        private static IEnumerable<ReplacementPair> basicreplacements = new List<ReplacementPair>
        {
            // addition
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Mul, Purpose=addToMul },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Sub, Purpose=addToSub },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Div, Purpose=addToDiv },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Rem, Purpose=addToRem },

            // subtraction
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Mul, Purpose=subToMul },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Add, Purpose=subToAdd },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Div, Purpose=subToDiv },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Rem, Purpose=subToRem },

            // multiplication
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Add, Purpose=mulToAdd },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Sub, Purpose=mulToSub },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Div, Purpose=mulToDiv },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Rem, Purpose=mulToRem },

            // division
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Add, Purpose=divToAdd },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Sub, Purpose=divToSub },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Mul, Purpose=divToMul },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Rem, Purpose=divToRem },

            // modulo
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Add, Purpose=remToAdd },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Sub, Purpose=remToSub },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Div, Purpose=remToDiv },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Mul, Purpose=remToMul },
        };

        private static IEnumerable<ReplacementPair> basicadditionreplacements = new List<ReplacementPair>
        {
            // addition
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Mul, Purpose=addToMul },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Sub, Purpose=addToSub },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Div, Purpose=addToDiv },
            new ReplacementPair{OldCode=OpCodes.Add,NewCode=OpCodes.Rem, Purpose=addToRem },
        };

        private static IEnumerable<ReplacementPair> basicsubtractionreplacements = new List<ReplacementPair>
        {
            // subtraction
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Mul, Purpose=subToMul },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Add, Purpose=subToAdd },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Div, Purpose=subToDiv },
            new ReplacementPair{OldCode=OpCodes.Sub,NewCode=OpCodes.Rem, Purpose=subToRem },
        };

        private static IEnumerable<ReplacementPair> basicmultiplicationreplacements = new List<ReplacementPair>
        {
            // multiplication
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Add, Purpose=mulToAdd },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Sub, Purpose=mulToSub },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Div, Purpose=mulToDiv },
            new ReplacementPair{OldCode=OpCodes.Mul,NewCode=OpCodes.Rem, Purpose=mulToRem },
        };

        private static IEnumerable<ReplacementPair> basicdivisionreplacements = new List<ReplacementPair>
        {
            // division
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Add, Purpose=divToAdd },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Sub, Purpose=divToSub },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Mul, Purpose=divToMul },
            new ReplacementPair{OldCode=OpCodes.Div,NewCode=OpCodes.Rem, Purpose=divToRem },
        };

        private static IEnumerable<ReplacementPair> basicmoduloreplacements = new List<ReplacementPair>
        {
            // modulo
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Add, Purpose=remToAdd },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Sub, Purpose=remToSub },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Div, Purpose=remToDiv },
            new ReplacementPair{OldCode=OpCodes.Rem,NewCode=OpCodes.Mul, Purpose=remToMul },
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

        private static IEnumerable<ReplacementPair> operandReplacements = new List<ReplacementPair>
        {
            new ReplacementPair{OldCode=OpCodes.Ldc_I4_0,NewCode=OpCodes.Ldc_I4_1 ,Purpose=insertNot },
            new ReplacementPair{OldCode=OpCodes.Ldc_I4_1,NewCode=OpCodes.Ldc_I4_0 ,Purpose=insertNot },
        };

        private static IEnumerable<OpCode> nottableCodes = new List<OpCode>
        {
            OpCodes.Cgt,
            OpCodes.Cgt_Un,
            OpCodes.Clt,
            OpCodes.Clt_Un,
            OpCodes.Ceq
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

        public static IEnumerable<IAbstractMutation<InstructionContext>> GetAllReinforcementInstructionMutations(string category, int[] operators)
        {
            // MAS 20210130
            int index = 0;
            var mutations = new List<IAbstractMutation<InstructionContext>>();

            // MAS 20210318
            // determine operators category
            // A=all replacements, B=basic replacements, C=conditional and operand replacements, D=method replacements, E=notable replacements
            // determine operators indicators
            // 1=operator enabled, 0=operator disabled

            switch (category)
            {
                case "A":
                    // replacements
                    foreach (var replacement in replacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "B":
                    // basic replacements
                    foreach (var replacement in basicreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "BA":
                    // basic addition replacements
                    foreach (var replacement in basicadditionreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "BS":
                    // basic subtraction replacements
                    foreach (var replacement in basicsubtractionreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "BM":
                    // basic multiplication replacements
                    foreach (var replacement in basicmultiplicationreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "BD":
                    // basic division replacements
                    foreach (var replacement in basicdivisionreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "BO":
                    // basic modulo replacements
                    foreach (var replacement in basicmoduloreplacements)
                    {
                        // MAS 20210130 - limit to specified operators
                        if (operators[index] == 1)
                        {
                            var forwardSteps = new ReplaceOperationOpCode(replacement.NewCode, replacement.OldCode);
                            //var backwardSteps = new ReplaceOperationOpCode(replacement.OldCode, replacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.OldCode), MutationSteps = forwardSteps, Purpose = replacement.Purpose };
                            //var abstractMutationB = new AbstractMutation<InstructionContext> { Family = aor, CodeFilter = i => i.Instruction.OpCode.Equals(replacement.NewCode), MutationSteps = backwardSteps, Purpose = replacement.Purpose };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                        index++;
                    }
                    break;

                case "C":
                    // conditional and operand replacements
                    foreach (var jumpReplacement in conditionalJumpReplacements)
                    {
                        foreach (var operandReplacement in operandReplacements)
                        {
                            var forwardSteps = new ReplaceOperationAndOperandOpCode(jumpReplacement.NewCode, jumpReplacement.OldCode, operandReplacement.NewCode, operandReplacement.OldCode);
                            //var backwardSteps = new ReplaceOperationAndOperandOpCode(jumpReplacement.OldCode, jumpReplacement.NewCode, operandReplacement.OldCode, operandReplacement.NewCode);
                            var abstractMutationF = new AbstractMutation<InstructionContext>
                            {
                                Family = ror,
                                CodeFilter = i => i.Instruction.OpCode.Equals(jumpReplacement.OldCode) &&
        ((Instruction)i.Instruction.Operand).OpCode.Equals(operandReplacement.OldCode)
                            ,
                                MutationSteps = forwardSteps,
                                Purpose = jumpReplacement.Purpose
                            };
        //                    var abstractMutationB = new AbstractMutation<InstructionContext>
        //                    {
        //                        Family = ror,
        //                        CodeFilter = i => i.Instruction.OpCode.Equals(jumpReplacement.NewCode) &&
        //((Instruction)i.Instruction.Operand).OpCode.Equals(operandReplacement.NewCode)
        //                    ,
        //                        MutationSteps = backwardSteps,
        //                        Purpose = jumpReplacement.Purpose
        //                    };
                            mutations.Add(abstractMutationF);
                            //mutations.Add(abstractMutationB);
                        }
                    }
                    break;

                case "D":
                    // method replacements
                    foreach (var replacement in methodReplacements)
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
                    break;

                case "E":
                    // notable replacements
                    foreach (OpCode code in nottableCodes)
                    {
                        var forwardSteps = new InsertFollowingNot();
                        var abstractMutationF = new AbstractMutation<InstructionContext> { Family = ror, CodeFilter = i => i.Instruction.OpCode.Equals(code), MutationSteps = forwardSteps, Purpose = insertNot };
                        mutations.Add(abstractMutationF);
                    }
                    break;

            }


            return mutations;
        }

        public static IEnumerable<IAbstractMutation<FieldContext>> GetAllReinforcementFieldMutations()
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
