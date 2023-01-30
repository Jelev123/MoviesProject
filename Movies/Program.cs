using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Contract.Genre;
using Movies.Core.Contract.Movie;
using Movies.Core.Contract.Search;
using Movies.Core.Contracts.Video;
using Movies.Core.Service.Movie;
using Movies.Core.Service.Video;
using Movies.Core.Services.Genre;
using Movies.Core.Services.Search;
using Movies.Infrastructure.Data;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddTransient<IVideoService, VideoService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<ISearchService, SearchService>();


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



app.UseHttpsRedirection();
//app.UseStaticFiles();

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".vtt"] = "text/vtt";
provider.Mappings[".srt"] = "text/srt";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider

});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
public static class SessionExtensions
{
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
}
