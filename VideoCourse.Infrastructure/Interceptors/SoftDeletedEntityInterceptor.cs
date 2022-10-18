using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Infrastructure.Interceptors;

public class SoftDeletedEntityInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? context = eventData.Context;
        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        // Get All Entities
        var entities = context.ChangeTracker
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

        return base.SavingChangesAsync(eventData, result, cancellationToken);
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