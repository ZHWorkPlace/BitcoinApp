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

builder.Services.AddSingleton<BitcoinApp.Server.Services.RetrievedValuesService>();

// HTTP client used by the background worker
builder.Services.AddHttpClient("BitcoinWorkerClient");

// Register the periodic background worker
builder.Services.AddHostedService<BitcoinApp.Server.Services.BitcoinWorker>();

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
