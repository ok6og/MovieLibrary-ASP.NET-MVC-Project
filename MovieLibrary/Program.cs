using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data;
using MovieLibrary.Infrastructure;
using MovieLibrary.Services.Movies;
using MovieLibrary.Services.Statistics;
using MovieLibrary.Services.TicketSellers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MovieLibraryDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<MovieLibraryDbContext>();
builder.Services.AddControllersWithViews(options =>
        options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>()
    ); 
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
app.MapDefaultControllerRoute();
app.MapRazorPages();



app.Run();
