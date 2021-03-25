using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockToDB.EntityFramework;

namespace BlockToDB.Application
{
    public class DbSession
    {
        [Inject]
        public MainDatabaseContext MainDatabaseContext { get; set; }
        private DbContextTransaction _transaction = null;
        private string _interceptionId;

        public DbSession()
        {

        }
        public void SetTransaction(string id)
        {
            if (_interceptionId == null)
            {
                _interceptionId = id;
                _transaction = MainDatabaseContext.Database.BeginTransaction();
            }

        }

        public async Task CommitTransactionAsync(string id)
        {
            if (_interceptionId == id)
            {
                _interceptionId = null;
                int data = await MainDatabaseContext.SaveChangesAsync();
                if (_transaction != null)
                {
                    _transaction.Commit();
                    _transaction = null;
                }
            }

        }

        public void CommitTransaction(string id)
        {
            if (_interceptionId == id)
            {
                _interceptionId = null;
                MainDatabaseContext.SaveChanges();
                if (_transaction != null)
                {
                    _transaction.Commit();
                    _transaction = null;
                }
            }

        }
        public void RollbackTransaction(string id)
        {
            if (_interceptionId == id)
            {
                _interceptionId = null;
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction = null;
                }
            }

        }
    }
}
