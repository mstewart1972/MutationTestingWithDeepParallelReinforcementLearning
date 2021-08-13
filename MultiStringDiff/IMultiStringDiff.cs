using MutantCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public interface IMultiStringDiff<KeyType>
    {
        string BaseString { get; }
        void AddString(KeyType key, string newString);
        string Version(KeyType key);
        IList<StringSectionModel> GetModel(Func<KeyType, string> keyMap);
    }
}
