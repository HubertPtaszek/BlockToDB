using System.Collections.Generic;

namespace BlockToDB.Infrastructure
{
    public class GetDataResult : GetDataResult<dynamic>
    {
        public GetDataResult() : base()
        {

        }
    }

    public class GetDataResult<TEntity>
    {
        public GetDataResult()
        {
            ItemList = new List<TEntity>();
        }
        public List<TEntity> ItemList { get; set; }
        public long TotalCount { get; set; }
    }
}
