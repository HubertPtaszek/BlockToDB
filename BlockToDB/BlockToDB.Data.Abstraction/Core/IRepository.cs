using BlockToDB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false);
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteWhere(Expression<Func<TEntity, bool>> whereCondition);
        void Attach(TEntity entity);
        void Detach(TEntity entity);
        IList<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false);
        IList<TEntity> GetAll(bool tracking = false);
        int Count(Expression<Func<TEntity, bool>> whereCondition);
        int Count();
        bool Any(Expression<Func<TEntity, bool>> whereCondition);
        bool Any();

        void AddOrEdit<TEn>(TEn entity) where TEn : Entity, TEntity;
        TEntity GetSingle(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false);
        void Edit(TEntity entity);
        void EditRange(List<TEntity> entities);

        int Save();
        Task<int> SaveAsync();
        TEntity Find(int id);
       }
    
    public interface IRepository
    {
    }
}
