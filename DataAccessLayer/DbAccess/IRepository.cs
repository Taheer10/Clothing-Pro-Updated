using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingPro.DataAccessLayer.DbAccess;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<ICollection<TEntity>> GetByAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetAll();
    TEntity Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void UpdateRange(IEnumerable<TEntity> entities);
    void EditRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    void DeleteAll(IEnumerable<TEntity> entity);
    IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate);
    TEntity FindBy(Expression<Func<TEntity, bool>> predicate);
    TEntity Last();
    TEntity First();
    TEntity FindById(object id);
    IEnumerable<TEntity> FromSqlRaw(string sqlquery);
    IEnumerable<TEntity> FromSqlRaw(string sqlquery, bool isStoreProc, params SqlParameter[] parameters);
    void ExecuteSqlRaw(string sqlquery, bool isStoreProc, params SqlParameter[] parameters);
    object ExecuteScalar(string sqlquery, bool isStoreProc, SqlParameter[] sqlParameters);
    DataSet DataSetSqlQuery(string sqlquery, bool isStoreProc, SqlParameter[] sqlParameters = null);
    void SetEntityState(TEntity entity, EntityState state);

    // New method to detach an entity
    void Detach(TEntity entity);
}
