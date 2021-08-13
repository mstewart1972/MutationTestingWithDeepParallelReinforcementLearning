using MutantCommon;
using MutantTesting.CommandLineInterface.FlagParameters;
using MutantTesting.CommandLineInterface.ValueParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MutantTesting.CommandLineInterface
{
    public class CLIConsole
    {
        private static NLog.Logger Logger { get { return NLog.LogManager.GetCurrentClassLogger(); } }

        public void Write(String message)
        {
            Console.WriteLine(message);
        }

        public void Write()
        {
            Console.WriteLine();
        }

        private List<FlagParameter> flagParameters = new List<FlagParameter> {
                new BuildFlag(),
                new VerboseFlag() };

        private List<ValueParameter> valueParameters = new List<ValueParameter> {
                new AgentNumberParameter(),
                new BatchSizeParameter(),
                new ReportFolderParameter(),
                new SolutionParameter(),
                new SourceFileParameter(),
                new WorkingFolderParameter(),
                new OperatorParameter(),
                new CategoryParameter()
                };

        public void DisplayHelp()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            var descriptionAttribute = assembly
           .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
           .OfType<AssemblyDescriptionAttribute>()
           .FirstOrDefault();

            var versionAttribute = assembly
               .GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)
               .OfType<AssemblyFileVersionAttribute>()
               .FirstOrDefault();

            var copyRightAttribute = assembly
               .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false)
               .OfType<AssemblyCopyrightAttribute>().FirstOrDefault();

            if (copyRightAttribute != null)
                Write(copyRightAttribute.Copyright);
            if (versionAttribute != null)
                Write(ConstantsDeclaration.VERSION_KEY_WORD + versionAttribute.Version);
            if (descriptionAttribute != null)
                Write(descriptionAttribute.Description);

            Write();
            Write(ConstantsDeclaration.PARAMETERS_HELP_START);


            string Name;
            string Description;
            string Key;
            string FormatInstruction;

            //Write flags
            foreach(var parameter in flagParameters)
            {
                Name = parameter.Name;
                Description = parameter.Description;
                Key = parameter.Key;
                Write($"{Name}: {Description}\nUse: {Key}");
                Write();
            }
            foreach (var parameter in valueParameters)
            {
                Name = parameter.Name;
                Description = parameter.Description;
                Key = parameter.Key;
                FormatInstruction = parameter.FormatInstructions;
                Write($"{Name}: {Description}\nUse: {Key}={FormatInstruction}");
                if (parameter.IsMandatory)
                {
                    Write("This parameter is mandatory");
                }
                Write();
            }
        }

        public InputParameters ParseInput(string[] args, InputParameters defaults)
        {
            var parameters = defaults;
            var setValues = new Dictionary<string, string>();
            var setFlags = new HashSet<string>();

            foreach (string arg in args)
            {
                int equalPosition = arg.IndexOf(ConstantsDeclaration.EQUAL_OPERATOR);
                if (equalPosition == -1)
                {
                    //Argument is a flag
                    setFlags.Add(arg);
                }
                else
                {
                    //Argument has a value
                    string argumentName = arg.Substring(0, equalPosition);
                    string argumentValue = arg.Substring(equalPosition + 1);
                    setValues.Add(argumentName, argumentValue);
                }
            }

            //Check for help
            var helpFlags = new HashSet<string> { "-h", "-help", "--h", "help", "h", "?", "-?" };
            if (setFlags.Overlaps(helpFlags))
            {
                DisplayHelp();
                parameters.ShouldRun = false;
                return parameters;
            }

            //Check flags
            foreach (var flag in flagParameters)
            {
                flag.UpdateParametersIfContainsFlag(parameters, setFlags);
            }

            //Check value parameters
            bool valueParameterAssigned;
            foreach (var valueParameter in valueParameters)
            {
                valueParameterAssigned = valueParameter.UpdateWithValue(parameters, setValues);
                if (!valueParameterAssigned && valueParameter.IsMandatory)
                {
                    parameters.ShouldRun = false;
                    Write(string.Format(ConstantsDeclaration.MISSING_PARAMETER_MESSAGE, valueParameter.Name));
                }
            }

            return parameters;
        }

    }
}
