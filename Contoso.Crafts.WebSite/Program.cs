using Contoso.Crafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
// Add our new service and controllers
builder.Services.AddControllers();
builder.Services.AddTransient<JsonFileProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//add simply API
//I can see entire JSON file if i put "/products" to url
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    //map controlers
    endpoints.MapControllers();
    //endpoints.MapGet("/products", (context) =>
    //{
    //    var products = ((IApplicationBuilder)app).ApplicationServices.GetService<JsonFileProductService>().GetProducts();
    //    var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
    //    return  context.Response.WriteAsync(json);
    //});
});

app.Run();
