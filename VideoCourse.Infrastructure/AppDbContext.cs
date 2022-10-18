using System.Data;
using System.Reflection;
using ErrorOr;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common.DbContextExtensions;

namespace VideoCourse.Infrastructure;

public class AppDbContext : DbContext, IDbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions options)
    :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
        
        modelBuilder.LowercaseTablesAndProperties();
    }
    
        

    public new DbSet<TEntity> Set<TEntity>() where TEntity : Entity
    {
        return base.Set<TEntity>();
    }

    public DbSet<TEntity> SetNoEntity<TEntity>() where TEntity : class
    {
        return base.Set<TEntity>();
    }

    public async Task<ErrorOr<TEntity?>> GetByIdAsync<TEntity>(Guid id) 
        where TEntity : Entity
    {
        return id == Guid.Empty
            ? CustomErrors.Entity.EmptyGuid
            : await Set<TEntity>().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ErrorOr<TEntity>> Insert<TEntity>(TEntity entity) where TEntity : Entity
    {
        var entry = await Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public new async Task<ErrorOr<bool>> Remove<TEntity>(TEntity entity) where TEntity : Entity
    {
        Set<TEntity>().Remove(entity);
        return true;
    }

    public async Task<ErrorOr<bool>> InsertRange<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : Entity
    {
        
        await Set<TEntity>().AddRangeAsync(entities);
        return true;
    }

    public IDbContextTransaction GetTransaction()
    {
        return Database.BeginTransaction();
    }

    public async Task<IEnumerable<TEntity>> GetRecordsUsingRawSqlAsync<TEntity>(string query, object parameters)
    {
        
            var results = await Database.GetDbConnection().QueryAsync<TEntity>(
                sql: query,
                param: parameters,
                commandType: CommandType.Text);

            return results;

    }

    public async Task<ErrorOr<TEntity>> GetFirstUsingRawSqlAsync<TEntity>(string query, object parameters)
    {
        try
        {
            var result = await Database.GetDbConnection().QueryFirstAsync<TEntity>(
                sql: query,
                param: parameters,
                commandType: CommandType.Text);

            return result;
        }
        catch (Exception ex)
        {
            return Error.Failure(
                code: "Error occur",
                description: ex.Message);
        }
    }

    public async Task<int> ExecuteSqlAsync(string query, object parameters)
    {
        // Get current transaction
        var transaction = GetTransaction();
        var results = await Database.GetDbConnection().ExecuteAsync(
            sql: query,
            param: parameters,
            commandType: CommandType.Text,
            transaction: transaction.GetDbTransaction());

        return results;
    }

    public async Task<SqlMapper.GridReader> GetRecordUsingMultipleQueries(string query, object param)
    {
        var results = await Database.GetDbConnection().QueryMultipleAsync(
            sql: query,
            param: param,
            commandType: CommandType.Text);

        return results;
    }

    public async Task<int> Commit()
    {
        return await SaveChangesAsync();
    }

    public new async Task<bool> Dispose()
    {
        await DisposeAsync();
        return true;
    }
}