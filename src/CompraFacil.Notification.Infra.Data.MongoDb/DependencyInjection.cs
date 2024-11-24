using CompraFacil.Notification.Domain.Entities;
using CompraFacil.Notification.Domain.Entities.Abstraction;
using CompraFacil.Notification.Domain.Repositories;
using CompraFacil.Notification.Infra.Configurations;
using CompraFacil.Notification.Infra.Data.MongoDb.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace CompraFacil.Notification.Infra.Data.MongoDb;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddMongoDB(this IServiceCollection services, AppConfiguration appConfiguration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            return new MongoClient(appConfiguration.MongoDb?.ConnectionString);
        });

        services.AddTransient<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(appConfiguration.MongoDb?.Database);
        });


        services.AddRepositories();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddMongoRepository<EmailTemplate>("email-templates");

        services.AddScoped<IMailRepository, MailRepository>();

        return services;
    }

    private static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collection) where T : IEntityBase
    {
        services.AddScoped<IMongoRepository<T>>(f =>
        {
            var mongoDatabase = f.GetRequiredService<IMongoDatabase>();

            return new MongoRepository<T>(mongoDatabase, collection);
        });

        return services;
    }
}
