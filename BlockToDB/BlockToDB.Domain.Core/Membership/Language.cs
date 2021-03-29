using BlockToDB.Dictionaries;

namespace BlockToDB.Domain
{
    public class Language : Entity
    {
        public LanguageDictionary LanguageDictionary { get; set; }
        public string CultureSymbol { get; set; }
    }
}
