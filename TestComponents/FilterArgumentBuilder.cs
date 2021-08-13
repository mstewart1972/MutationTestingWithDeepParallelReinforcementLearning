using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestComponents
{
    public class FilterArgumentBuilder
    {
        private const string OR = "|";
        private const string FULL_TEST_NAME_CONTAINS = "FullyQualifiedName~";
        private const string FILTER_COMMAND = "--filter ";
        private const string QUOTEMARK = "\"";

        public string buildFilterArgument(IEnumerable<Unittest> tests)
        {
            if (tests.Any()) { return ""; }
            var filterArgument = new StringBuilder();
            filterArgument.Append(FILTER_COMMAND);
            filterArgument.Append(QUOTEMARK);
            bool firstTime = true;
            foreach (var test in tests)
            {
                if (firstTime)
                {
                    firstTime = false;
                }
                else
                {
                    filterArgument.Append(OR);
                }
                filterArgument.Append(FULL_TEST_NAME_CONTAINS);
                filterArgument.Append(test.Name);
            }
            filterArgument.Append(QUOTEMARK);
            return filterArgument.ToString();
        }
    }
}
