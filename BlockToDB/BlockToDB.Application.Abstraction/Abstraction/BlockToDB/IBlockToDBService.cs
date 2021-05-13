using BlockToDB.Application.Abstraction;
using DevExtreme.AspNet.Data;

namespace BlockToDB.Application
{
    public interface IBlockToDBService : IService
    {
        object GetSchemasToList(DataSourceLoadOptionsBase loadOptions);

        int GenerateScript(BlockToDBGenerateVM model);

        int Add(BlockToDBAddVM model);

        void Delete(int id);

        void Edit(BlockToDBEditVM model);

        BlockToDBEditVM GetToEdit(int id);

        DownloadVM DownloadFile(int id, bool isScript);

        BlockToDBVM GetBlockToDBVM(int id);
    }
}