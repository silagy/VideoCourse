using System.Text;
using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using VideoCourse.Application.Core.Abstractions.Authentication;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Application.Core.Abstractions.Cryptography;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Emails;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Infrastructure.BackgroundJobs;
using VideoCourse.Infrastructure.Common;
using VideoCourse.Infrastructure.Common.Cryptography;
using VideoCourse.Infrastructure.Common.DapperSqlCasting;
using VideoCourse.Infrastructure.Interceptors;
using VideoCourse.Infrastructure.Repositories;
using VideoCourse.Infrastructure.Services.Emails;

namespace VideoCourse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Default")!;
        services.AddSingleton<UpdateAuditableEntityInterceptor>();
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddSingleton<SoftDeletedEntityInterceptor>();
        
        SqlMapper.AddTypeHandler(new VideoUrlHandler());
        SqlMapper.AddTypeHandler(new DurationHandler());
        SqlMapper.AddTypeHandler(new EmailDapperConvertHandler());

        services.AddQuartz(quartzConfigurator =>
        {
            var jobkey = new JobKey(nameof(ProcessOutboxMessages));

            quartzConfigurator
                .AddJob<ProcessOutboxMessages>(jobkey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobkey)
                        .WithSimpleSchedule(
                            schedule =>
                                schedule.WithIntervalInSeconds(10)
                                    .RepeatForever()));

            quartzConfigurator.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            //var auditableEntityInterceptor = sp.GetService<UpdateAuditableEntityInterceptor>();
            //var convertDomainEventsToOutboxMessagesInterceptor =
                //sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
            //var softDeleteEntitiesInterceptor = sp.GetService<SoftDeletedEntityInterceptor>();
            options.UseNpgsql(connectionString);
            //.AddInterceptors(auditableEntityInterceptor!)
            //.AddInterceptors(convertDomainEventsToOutboxMessagesInterceptor!)
            //.AddInterceptors(softDeleteEntitiesInterceptor!);
        });
        
        services.AddScoped<IDbContext>(serviceProvider => serviceProvider.GetService<AppDbContext>()!);
        //services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetService<AppDbContext>()!);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordHashChecker, PasswordHasher>();
        services.AddTransient<IDateTime, DateTimeProvider>();
        services.AddScoped<IEmailService, EmailService>();
       
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>();

        services.AddAuth(configuration);
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        services.AddSingleton(Options.Create(jwtSettings));
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(
                defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        
        return services;
    }
}