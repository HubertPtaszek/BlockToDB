using BlockToDB.Dictionaries;
using BlockToDB.Domain;
using BlockToDB.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Data
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Language GetLanguage(LanguageDictionary languageDictionary);
        List<SelectModelBinder<int>> GetLanguagesToSelect();
    }
}
