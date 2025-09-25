using Api.Configuration;
using Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppDatabase(builder.Configuration, builder.Environment);
builder.Services.AddAppServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI();

app.MapHealthEndpoints();
app.MapPriorAuthEndpoints();

app.Run();

public partial class Program { }
