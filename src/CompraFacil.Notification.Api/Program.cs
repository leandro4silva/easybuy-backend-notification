using CompraFacil.Notification.Infra;
using CompraFacil.Notification.Infra.Extensions;
using CompraFacil.Notification.Infra.Data.MongoDb;
using CompraFacil.Notification.Infra.MessageBus;

var builder = WebApplication.CreateBuilder(args);

var appConfigs = builder.AddAppConfigs();

await builder.Services.AddRabbitMqAsync(appConfigs);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddMongoDB(appConfigs)
    .AddMailService(appConfigs)
    .AddSubscribers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
