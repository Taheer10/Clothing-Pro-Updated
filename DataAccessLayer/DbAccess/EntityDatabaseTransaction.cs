using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ClothingPro.DataAccessLayer.DbAccess;
public class EntityDatabaseTransaction
{
    
    private IDbContextTransaction _transaction;

    public EntityDatabaseTransaction(DbContext context)
    {
        _transaction = context.Database.BeginTransaction();

        
    }
    public void Commit()
    {
       
        
        _transaction.Commit();
    }

    public void Rollback()
    {
        _transaction.Rollback();
    }
    public void Dispose()
    {
        _transaction.Dispose();
    }

}
