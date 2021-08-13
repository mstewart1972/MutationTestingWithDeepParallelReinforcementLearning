using ExternalProgramExecution;
using System;
using System.Runtime.Serialization;

namespace SolutionBuilder
{
    [Serializable]
    public class BuildException : ProcessException
    {
        public BuildException()
        {
        }

        public BuildException(string message) : base(message)
        {
        }

        public BuildException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BuildException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}