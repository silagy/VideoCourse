using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Quartz;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Domain.Abstractions;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessages : IJob
{
    private readonly AppDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly IDateTime _dateTimeProvider;

    public ProcessOutboxMessages(AppDbContext dbContext, IPublisher publisher, IDateTime dateTimeProvider)
    {
        _dbContext = dbContext;
        _publisher = publisher;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _dbContext
            .SetNoEntity<OutboxMessage>()
            .Where(message => message.PublishedOnUtc == null && message.Error == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            IDomainEvent? domainEvent;
            try
            {
                domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });


                if (domainEvent is null)
                {
                    continue;
                }

                await _publisher.Publish(domainEvent, context.CancellationToken);
                message.PublishedOnUtc = _dateTimeProvider.UtcNow;
            }
            catch (Exception e)
            {
                message.PublishedOnUtc = _dateTimeProvider.UtcNow;
                message.Error = e.Message.ToString();
            }
        }

        if (messages.Any())
        {
            await _dbContext.SaveChangesAsync();
        }
        
    }
}