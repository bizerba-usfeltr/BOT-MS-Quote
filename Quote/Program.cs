using System.Reflection;
using Infrastructure.Contexts;
using Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "BOT2.0 Quotes",
        Description = "Microservice to support the creation and editing of quotes"
    });
    
    var xmlFile = "Domain.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<QuoteContext>(options => options.UseNpgsql(
    @"Server=localhost;Port=5454;Database=botdevquotedatastore;User Id=postgres;Password=Bizerba1;"));
builder.Services.AddTransient<IQuoteRepository, QuoteRepository>();

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