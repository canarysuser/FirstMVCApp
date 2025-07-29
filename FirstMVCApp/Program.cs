using FirstMVCApp.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//reads the appsettings.json file and gets the connection string    
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Register the DbContext with the DI container
builder.Services.AddDbContext<NorthwindDbContext>(options => {
    options.UseSqlServer(connectionString);
    options.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
});

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout to 30 minutes
    options.Cookie.HttpOnly = true; // Make the session cookie HTTP-only
    options.Cookie.IsEssential = true; // Make the session cookie essential for the application\
   
});

//builder.Services.AddScoped<IProductRepository, ProductListRepository>();
builder.Services.AddScoped<IProductRepository, ProductEFRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository   >();

builder.Services.AddSingleton<DependencyClass>();

builder.Services.AddControllersWithViews();
/*builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
//http://localhost:4000/            => controller = Home, Action = INdex, id=null
//http://localhost:4000/Page        => controller = Page, Action = Index, id=null
//http://localhost:4000/P1/P2       => controller = P1, Action = P2, id=null
//http://localhost:4000/P1/P2/P3    => controller = P1, Action = P2, id=P3



app.Run();
