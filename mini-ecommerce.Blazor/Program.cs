using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using mini-ecommerce.Blazor;
using mini-ecommerce.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
    new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<OrderService>();

await builder.Build().RunAsync();