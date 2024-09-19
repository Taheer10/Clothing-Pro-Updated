using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClothingPro.DataAccessLayer.DataContext;

namespace ClothingPro.DataAccessLayer.DbAccess;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ClothingProContext _dataContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(ClothingProContext context)
    {
        this._dataContext = context;
        this._dbSet = context.Set<TEntity>();
        //this._dataContext.LazyLoadingEnabled = false;
        //this._dataContext.Configuration.AutoDetectChangesEnabled = false;
        //this._dataContext.Configuration.ValidateOnSaveEnabled = true;
    }

    public TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);
        _dataContext.SaveChanges();
        return entity;
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
        _dataContext.SaveChanges();
    }

    public void Update(TEntity entity)
    {
        _dbSet.Attach(entity);
        _dataContext.Update(entity);
        _dataContext.SaveChanges();
    }
    public void EditRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        _dataContext.SaveChanges();
    }
    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        _dataContext.SaveChanges();
    }

    public void DeleteAll(IEnumerable<TEntity> entity)
    {
        _dbSet.RemoveRange(entity);
        _dataContext.SaveChanges();
    }
    public virtual async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _dataContext.Set<TEntity>().ToListAsync();
    }
    public virtual async Task<ICollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dataContext.Set<TEntity>().Where(predicate).ToListAsync();
    }
    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public TEntity FindBy(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }
    public TEntity Last()
    {
        return _dbSet.ToList().Last();
    }
    public TEntity First()
    {
        return _dbSet.First();
    }
    public TEntity FindById(object id)
    {
        return _dbSet.Find(id);
    }

    public void ExecuteSqlRaw(string sqlquery, bool isStoreProc = false, params SqlParameter[] parameters)
    {
        if (isStoreProc)
        {
            _dataContext.Database.ExecuteSqlRaw($"EXEC {sqlquery}", parameters);
        }
        else
        {
            _dataContext.Database.ExecuteSqlRaw(sqlquery, parameters);
        }
    }

    //only gets data wont update. get query to fill in object.
    public IEnumerable<TEntity> FromSqlRaw(string sqlquery)
    {
        return _dataContext.Set<TEntity>().FromSqlRaw(sqlquery);
    }

    //only gets data wont update. get query to fill in object.
    public IEnumerable<TEntity> FromSqlRaw(string sqlquery, bool isStoreProc = false, params SqlParameter[] parameters)
    {
        StringBuilder strBuilder = new StringBuilder();
        if (isStoreProc)
        {
            return _dataContext.Set<TEntity>().FromSqlRaw($"EXECUTE {sqlquery}", parameters);
        }
        else
        {
            return _dataContext.Set<TEntity>().FromSqlRaw(sqlquery, parameters);
        }
    }

    //sql query get datareader // get query to fill in datareader
    public object ExecuteScalar(string sqlquery, bool isStoreProc, SqlParameter[] sqlParameters)
    {
        DbCommand command = _dataContext.Database.GetDbConnection().CreateCommand();
        _dataContext.Database.OpenConnection();
        command.CommandText = sqlquery;

        if (sqlParameters != null)
        {
            command.Parameters.AddRange(sqlParameters);
        }

        if (isStoreProc)
        {
            command.CommandType = System.Data.CommandType.StoredProcedure;
        }
        var obj = command.ExecuteScalar();
        command.Dispose();
        _dataContext.Database.CloseConnection();
        return obj;
    }
    public DataSet DataSetSqlQuery(string sqlquery, bool isStoreProc, SqlParameter[] sqlParameters)
    {
        var connection = _dataContext.Database.GetDbConnection();
        var adp = DbProviderFactories.GetFactory(connection).CreateDataAdapter();
        DbCommand command = connection.CreateCommand();
        command.CommandText = sqlquery;

        if (sqlParameters != null)
        {
            command.Parameters.AddRange(sqlParameters);
        }

        if (isStoreProc)
        {
            command.CommandType = CommandType.StoredProcedure;
        }
        DataSet dataSet = new DataSet();
        adp.SelectCommand = command;
        adp.Fill(dataSet);
        command.Dispose();
        connection.Close();
        return dataSet;
    }
}