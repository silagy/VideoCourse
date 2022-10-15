using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Domain.Primitives;

namespace VideoCourse.Infrastructure.Interceptors;

public class UpdateAuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTimeProvider;

    public UpdateAuditableEntityInterceptor(IDateTime dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var enties = dbContext.ChangeTracker
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
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    
}