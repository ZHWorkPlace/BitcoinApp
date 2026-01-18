using BitcoinApp.Server.Database;
using BitcoinApp.Server.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext());

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI for controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Configure EF Core
builder.Services.AddDbContext<BitcoinDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BitcoinDatabase")));

// Register repository
builder.Services.AddScoped<IBitcoinValueRepository, BitcoinValueRepository>();

// Register RetrievedValues storage
builder.Services.AddSingleton<IRetrievedValuesService, RetrievedValuesService>();

// Register API service
builder.Services.AddTransient<BitcoinValuesApiService>();

// HTTP client used by the background worker
builder.Services.AddHttpClient("BitcoinWorkerClient");

// Register the periodic background worker
builder.Services.AddHostedService<BitcoinWorker>();

var app = builder.Build();

// Enable Swagger UI and OpenAPI endpoint
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "BitcoinApp API V1");
    c.RoutePrefix = "swagger";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Map minimal API OpenAPI document (if you use minimal endpoints in addition to controllers)
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
