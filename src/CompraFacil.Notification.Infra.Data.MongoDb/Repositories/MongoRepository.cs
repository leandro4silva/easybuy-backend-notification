﻿using CompraFacil.Notification.Domain.Entities.Abstraction;
using CompraFacil.Notification.Domain.Repositories;
using MongoDB.Driver;

namespace CompraFacil.Notification.Infra.Data.MongoDb.Repositories;

public sealed class MongoRepository<T> : IMongoRepository<T> where T : IEntityBase
{
    public IMongoCollection<T> Collection { get; private set; }

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        Collection = database.GetCollection<T>(collectionName);
    }

    public async Task<T> GetAsync(string @event)
    {
        return await Collection.Find(c => c.Event == @event).SingleOrDefaultAsync();
    }
}