using Api.Configuration;
using Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDatabase(builder.Configuration);
builder.Services.AddAppServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase();
app.UseSwagger();
app.UseSwaggerUI();

app.MapPriorAuthEndpoints();

app.Run();

public partial class Program { }
