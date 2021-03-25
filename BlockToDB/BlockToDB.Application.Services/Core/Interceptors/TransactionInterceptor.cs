using Ninject.Extensions.Interception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Application
{
    public class TransactionInterceptor : IInterceptor
    {
        public TransactionInterceptor()
        {

        }

        private static async Task InterceptAsync(Task originalTask, DbSession dbSession, string id)
        {
            // Await for the original task to complete       
            try
            {
                await originalTask;
                // asynchronous post-execution
                await dbSession.CommitTransactionAsync(id);
            }
            catch (Exception ex)
            {
                dbSession.RollbackTransaction(id);
                throw;
            }

        }

        private static async Task<T> InterceptParamAsync<T>(Task<T> originalTask, DbSession dbSession, string id, Type type)
        {
            // Await for the original task to complete
            try
            {
                T result = await originalTask;

                // asynchronous post-execution
                await dbSession.CommitTransactionAsync(id);

                return result;
            }
            catch (Exception ex)
            {
                dbSession.RollbackTransaction(id);
                throw;
            }
        }

        public void Intercept(IInvocation invocation)
        {

            DbSession dbSession = (invocation.Request.Target as ServiceBase).DbSession;
            string id = Guid.NewGuid().ToString();
            try
            {
                dbSession.SetTransaction(id);
                invocation.Proceed();

                if (invocation.ReturnValue != null)
                {
                    Type type = invocation.ReturnValue.GetType();
                    if (type.BaseType == typeof(Task))
                    {


                        if (!type.IsGenericType)
                        {
                            invocation.ReturnValue = InterceptAsync((Task)invocation.ReturnValue, dbSession, id);
                        }
                        else
                        {
                            Type defnition = type.GenericTypeArguments[0];
                            MethodInfo method = typeof(TransactionInterceptor).GetMethod("InterceptParamAsync", BindingFlags.NonPublic | BindingFlags.Static);
                            method = method.MakeGenericMethod(defnition);
                            invocation.ReturnValue = method.Invoke(null, new object[] { invocation.ReturnValue, dbSession, id, type }) as Task;
                        }
                        return;
                    }
                }
                dbSession.CommitTransaction(id);

            }
            catch (Exception ex)
            {
                dbSession.RollbackTransaction(id);
                throw;
            }
        }
    }
}
