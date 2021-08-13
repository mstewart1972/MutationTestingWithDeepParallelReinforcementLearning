using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public class MultiStringDiff<KeyType>:IMultiStringDiff<KeyType>
    {
        private readonly StringSection<KeyType> sectionsHead;
        private readonly IDiffer Differ;
        public string BaseString { get; private set; }

        public MultiStringDiff(string baseString, IDiffer differ)
        {
            sectionsHead = StringSection<KeyType>.BaseStringSection(baseString);
            Differ = differ;
            BaseString = baseString;
        }

        public void AddString(KeyType key, string newString)
        {
            var replacements = Differ.Diff(BaseString, newString);
            AddReplacements(key, replacements);
        }

        private void AddReplacement(KeyType key, IStringReplacement replacement)
        {
            var replacementlength = replacement.Length;
            var replacementPosition = replacement.Position;

            sectionsHead.AddAlternative(key, replacement.NewString, replacementPosition, replacementlength);
        }

        public string Version(KeyType key)
        {
            return sectionsHead.GetVersion(key);
        }

        private void AddReplacements(KeyType key, IEnumerable<IStringReplacement> replacements)
        {
            foreach(var replacement in replacements)
            {
                AddReplacement(key, replacement);
            }
        }

        public IList<StringSectionModel> GetModel(Func<KeyType, string> keyMap)
        {
            return sectionsHead.ToModelList(keyMap);
        }
    }
}
