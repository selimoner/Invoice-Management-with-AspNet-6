using ApiProject.Models;
using ApiProject.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<PostSystemDatabaseSettings>(
    builder.Configuration.GetSection(nameof(PostSystemDatabaseSettings)));

builder.Services.AddSingleton<IPostSystemDatabaseSettings>(
        sp=>sp.GetRequiredService<IOptions<PostSystemDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("PostSystemDatabaseSettings:ConnectionString")));

builder.Services.AddScoped<IPostService, PostService>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
