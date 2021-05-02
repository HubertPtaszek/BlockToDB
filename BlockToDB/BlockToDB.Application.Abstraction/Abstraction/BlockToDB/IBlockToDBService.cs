using BlockToDB.Application.Abstraction;
using DevExtreme.AspNet.Data;

namespace BlockToDB.Application
{
    public interface IBlockToDBService : IService
    {
        void Delete(int id);
        BlockToDBEditVM GetToEdit(int id);
        void Edit(BlockToDBEditVM model);
        void Add(BlockToDBAddVM model);
        object GetCaseToList(DataSourceLoadOptionsBase loadOptions);
    }
}