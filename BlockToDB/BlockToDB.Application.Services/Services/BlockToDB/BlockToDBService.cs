using Ninject;

namespace BlockToDB.Application
{
    public class BlockToDBService : ServiceBase, IBlockToDBService
    {
        #region Dependencies

        [Inject]
        public BlockToDBConverter BlockToDBConverter { get; set; }

        #endregion Dependencies
    }
}