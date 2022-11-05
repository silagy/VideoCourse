using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.Entities;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private readonly IDateTime _dateTimeProvider;

    public UnitOfWork(AppDbContext dbContext, IDateTime dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<int> Commit()
    {
        await ConvertDomainEventsToOutboxMessages();
        SoftDeleteEntities();
        UpdateAuditableEntities();
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Dispose()
    {
        await _dbContext.DisposeAsync();
        return await Task.FromResult(true);
    }

    private async Task ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .SelectMany(e =>
            {
                var domainEvents = e.GetDomainEvents();

                e.ClearDomainEvents();

                return domainEvents;
            })
            .Select(de => new OutboxMessage()
            {
                Id = Guid.NewGuid(),
                Type = de.GetType().Name,
                OccurredOnUtc = _dateTimeProvider.UtcNow,
                Content = JsonConvert.SerializeObject(de,
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();

        await _dbContext.SetNoEntity<OutboxMessage>().AddRangeAsync(outboxMessages);
        //_dbContext.Set<OutboxMessage>().AddRange(outboxMessages);

    }

    private void UpdateAuditableEntities()
    {
        var enties = _dbContext.ChangeTracker
            .Entries<IAuditableEntity>();
        foreach (var entityEntry in enties)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(p => p.CreationDate).CurrentValue = _dateTimeProvider.UtcNow;
                entityEntry.Property(p => p.UpdateDate).CurrentValue = _dateTimeProvider.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(p => p.UpdateDate).CurrentValue = _dateTimeProvider.UtcNow;
            }
        }
    }

    private void SoftDeleteEntities()
    {
        var entities = _dbContext.ChangeTracker
            .Entries<ISoftDeleteEntity>();

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Deleted)
            {
                entity.Property(nameof(ISoftDeleteEntity.IsDeleted)).CurrentValue = true;
                entity.State = EntityState.Modified;

                UpdateDeletedEntityReferencesToUnchanged(entity);
            }
        }
    }
    
    private static void UpdateDeletedEntityReferencesToUnchanged(EntityEntry entity)
    {
        if (!entity.References.Any())
        {
            return;
        }

        foreach (var referenceEntry in entity.References.Where(r => r.TargetEntry is not null && r.TargetEntry.State == EntityState.Deleted))
        {
            if (referenceEntry.TargetEntry != null)
            {
                referenceEntry.TargetEntry.State = EntityState.Unchanged;

                UpdateDeletedEntityReferencesToUnchanged(referenceEntry.TargetEntry);
            }
        }
    }
}