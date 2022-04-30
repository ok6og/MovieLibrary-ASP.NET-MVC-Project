using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Data.Models;
using MovieLibrary.Infrastructure.Extensions;
using MovieLibrary.Services.Actors;
using MovieLibrary.Services.Movies;
using MovieLibrary.Services.Statistics;
using MovieLibrary.Services.TicketSellers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieLibraryDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<MovieLibraryDbContext>();


builder.Services.AddControllersWithViews(options =>
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>()
    );
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IActorsService, ActorsService>();
builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<ITicketSellerService, TicketSellerService>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection()
    .PrepareDatabase()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "Movie Details",
    pattern: "/Movies/Details/{id}/{information}",
    defaults: new { controller = "Movie", action = "Details" });

app.MapRazorPages();
app.Run();