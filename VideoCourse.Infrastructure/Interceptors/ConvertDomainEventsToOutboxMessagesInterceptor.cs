using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Interceptors;

public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    private readonly IDateTime _dateTimeProvider;

    public ConvertDomainEventsToOutboxMessagesInterceptor(IDateTime dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        DbContext? context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var outboxMessages = context.ChangeTracker
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

        context.Set<OutboxMessage>().AddRange(outboxMessages);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}