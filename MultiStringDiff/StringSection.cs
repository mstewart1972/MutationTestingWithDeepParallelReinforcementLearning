using MutantCommon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiStringDiff
{
    public class StringSection<KeyType>
    {
        public string BaseString { get; private set; }
        public IDictionary<KeyType, string> Prefixes { get; }
        public IDictionary<KeyType, string> Alternatives { get; }
        public StringSection<KeyType> Next { get; private set; }
        public int Position { get; private set; }
        public int Length { get { return BaseString.Length; } }

        public static StringSection<KeyType> BaseStringSection(string baseString)
        {
            return new StringSection<KeyType>(baseString, 0, null);
        }

        private static StringSection<KeyType> TailStringSection(int position)
        {
            return new StringSection<KeyType>("", position, null);
        }

        private StringSection(string baseString, int position, StringSection<KeyType> next)
        {
            Next = next;
            Position = position;
            BaseString = baseString;
            Prefixes = new Dictionary<KeyType, string>();
            Alternatives = new Dictionary<KeyType, string>();
        }

        private void AddPrefix(KeyType key, string prefix)
        {
            Prefixes.Add(key, prefix);
        }

        private void AddAlternative(KeyType key, string alternative, int length)
        {
            if(length == 0)
            {
                AddPrefix(key, alternative);
            }else if (length < Length)
            {
                SplitSectionIndex(length);
                Alternatives.Add(key, alternative);
            }
            else if (length == Length)
            {
                Alternatives.Add(key, alternative);
            }
            else if (length > Length)
            {
                Alternatives.Add(key, alternative);
                Next.AddAlternative(key, String.Empty, length - Length);                
            }
        }

        public void AddAlternative(KeyType key, string alternative, int position, int length)
        {
            var section = GetSectionAtPosition(position);
            if(position > section.Position)
            {
                section.SplitAtStringPosition(position);
                section = section.Next;
            }
            section.AddAlternative(key, alternative, length);
        }

        private void SplitAtStringPosition(int position)
        {
            var splitIndex = position - Position;
            SplitSectionIndex(splitIndex);
        }

        private void SplitSectionIndex(int splitIndex)
        {
            var headString = BaseString.Substring(0, splitIndex);
            var tailString = BaseString.Substring(splitIndex);

            BaseString = headString;
            var tailSection = new StringSection<KeyType>(tailString,Position+Length, Next);
            foreach(var alternative in Alternatives)
            {
                tailSection.AddAlternative(alternative.Key, String.Empty, 0);
            }
            Next = tailSection;
        }

        public StringSection<KeyType> GetSectionAtPosition(int position)
        {
            var positionDifference = position - Position;
            if(positionDifference >= 0 && positionDifference < Length)
            {
                return this;
            }
            else if(positionDifference >= Length)
            {
                if(Next != null)
                {
                    return Next.GetSectionAtPosition(position);
                }
                else if (positionDifference == Length)
                {
                    if (Length == 0)
                    {
                        return this;
                    }
                    else
                    {
                        Next = TailStringSection(position);
                        return Next;
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException($"Index {position} is above the maximum index of the string");
                }
            }
            throw new IndexOutOfRangeException($"Index {position} comes before this section's position {Position}");
        }



        public string GetVersionLocal(KeyType versionKey)
        {
            var prefix = String.Empty;
            if (Prefixes.ContainsKey(versionKey))
            {
                prefix = Prefixes[versionKey];
            }
            var body = BaseString;
            if (Alternatives.ContainsKey(versionKey))
            {
                body = Alternatives[versionKey];
            }
            return prefix + body;
        }

        public string GetVersion(KeyType versionKey)
        {
            var builder = new StringBuilder();
            GetVersionIntoBuilder(versionKey, builder);
            return builder.ToString();
        }

        private void GetVersionIntoBuilder(KeyType key, StringBuilder builder)
        {

            builder.Append(GetVersionLocal(key));
            if(Next != null)
            {
                Next.GetVersionIntoBuilder(key, builder);
            }
        }

        private StringSectionModel ToModel(Func<KeyType, string> keyToString)
        {
            var prefixes = new Dictionary<string, string>();
            foreach(var prefix in Prefixes)
            {
                prefixes.Add(keyToString(prefix.Key), prefix.Value);
            }

            var alternatives = new Dictionary<string, string>();
            foreach (var alternative in Alternatives)
            {
                alternatives.Add(keyToString(alternative.Key), alternative.Value);
            }

            return new StringSectionModel { BaseString = BaseString, Alternatives = alternatives, Prefixes = prefixes };
        }

        private void FillModelList(IList<StringSectionModel> models, Func<KeyType, string> keyToString)
        {
            models.Add(ToModel(keyToString));
            if(Next != null)
            {
                Next.FillModelList(models, keyToString);
            }            
        }

        public IList<StringSectionModel> ToModelList(Func<KeyType, string> keyToString)
        {
            var models = new List<StringSectionModel>();
            FillModelList(models, keyToString);
            return models;
        }
    }
}
