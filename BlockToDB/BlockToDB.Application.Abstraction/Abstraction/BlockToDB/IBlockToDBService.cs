using BlockToDB.Application.Abstraction;

namespace BlockToDB.Application
{
    public interface IBlockToDBService : IService
    {
        int ConvertBlockToDB(string model, int type);

        CodeDTO DownloadFile(int id);
    }
}