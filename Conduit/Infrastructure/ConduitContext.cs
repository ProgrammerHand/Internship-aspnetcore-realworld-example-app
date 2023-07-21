using Conduit.Entities;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure
{
    public class ConduitContext : DbContext//:ApiAuthorizationDbContext<User>
    {
        //private IDbContextTransaction? _currentTransaction;

        public ConduitContext(DbContextOptions<ConduitContext> options)
            : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }


        //#region Transaction Handling
        //public void BeginTransaction()
        //{
        //    if (_currentTransaction != null)
        //    {
        //        return;
        //    }

        //    if (!Database.IsInMemory())
        //    {
        //        _currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        //    }
        //}

        //public void CommitTransaction()
        //{
        //    try
        //    {
        //        _currentTransaction?.Commit();
        //    }
        //    catch
        //    {
        //        RollbackTransaction();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (_currentTransaction != null)
        //        {
        //            _currentTransaction.Dispose();
        //            _currentTransaction = null;
        //        }
        //    }
        //}

        //public void RollbackTransaction()
        //{
        //    try
        //    {
        //        _currentTransaction?.Rollback();
        //    }
        //    finally
        //    {
        //        if (_currentTransaction != null)
        //        {
        //            _currentTransaction.Dispose();
        //            _currentTransaction = null;
        //        }
        //    }
        //}
        //#endregion
    }
}