using BlockToDB.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public interface IBlockToDBService : IService
    {
        int ConvertBlockToDB(string model, int type);
        CodeDTO DownloadFile(int id);
    }
}
