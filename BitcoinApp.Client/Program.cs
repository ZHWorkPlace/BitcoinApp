using BitcoinApp.Client;
using BitcoinApp.Client.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<BitcoinApiService>();

builder.Services.Configure<BitcoinApiSettings>(builder.Configuration.GetSection("BitcoinApiSettings"));

builder.Services.AddHttpClient<BitcoinApiService>((serviceProvider, client) =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<BitcoinApiSettings>>().Value;

    client.BaseAddress = new Uri(settings.ApiUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LiveData}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
