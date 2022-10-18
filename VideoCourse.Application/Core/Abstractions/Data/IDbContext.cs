using Dapper;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Domain.Entities;
using ErrorOr;
using Microsoft.EntityFrameworkCore.Storage;

namespace VideoCourse.Application.Core.Abstractions.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity: Entity;

    DbSet<TEntity> SetNoEntity<TEntity>()
        where TEntity : class;

    Task<ErrorOr<TEntity?>> GetByIdAsync<TEntity>(Guid id)
        where TEntity : Entity;

    Task<ErrorOr<TEntity>> Insert<TEntity>(TEntity entity)
        where TEntity: Entity;

    Task<ErrorOr<bool>> Remove<TEntity>(TEntity entity)
        where TEntity:Entity;

    Task<ErrorOr<bool>> InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities)
        where TEntity:Entity;

    IDbContextTransaction GetTransaction();

    Task<IEnumerable<TEntity>> GetRecordsUsingRawSqlAsync<TEntity>(string query, object parameters);

    Task<ErrorOr<TEntity>> GetFirstUsingRawSqlAsync<TEntity>(string query, object parameters);

    Task<int> ExecuteSqlAsync(string query, object parameters);
    Task<SqlMapper.GridReader> GetRecordUsingMultipleQueries(string query, object param);
}