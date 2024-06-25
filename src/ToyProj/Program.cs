using Microsoft.EntityFrameworkCore;
using ToyProj.Data;
using ToyProj.Services.Company.Repository;
using ToyProj.Services.Country.Repository;
using ToyProj.Services.Genre.Repository;
using ToyProj.Services.Keyword.Reposigory;
using ToyProj.Services.Movie.Repository;
using ToyProj.Services.MovieCast.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IMovieCastRepository, MovieCastRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IKeywordRepository, KeywordRepository>();

builder.Services.AddDbContext<DatabaseContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=admin}/{action=Index}/{id?}");

app.Run();
