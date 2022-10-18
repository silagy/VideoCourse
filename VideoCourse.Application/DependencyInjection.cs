using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VideoCourse.Application.Core.Abstractions.Emails.Settings;
using VideoCourse.Application.Core.Behaviors;

namespace VideoCourse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.Configure<MailSettings>(configuration.GetSection(MailSettings.SectionName));
        
        AddMappings(services);

        //services.AddScoped<IPipelineBehavior<CreateVideoCommand, ErrorOr<BasicVideoResponse>>, CreateVideoBehavior>();
        return services;
    }

    private static void AddMappings(IServiceCollection services)
    {
        // Add Mapster mapping
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}