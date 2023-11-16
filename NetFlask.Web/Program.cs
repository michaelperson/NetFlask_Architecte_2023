using NetFlask.DAL.Repository;
using NetFlask.DAL.Repository.Entities;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Récupération de la connection string
string cnstr = builder.Configuration.GetConnectionString("Dev");

//Injection des repos
builder.Services.AddScoped<IRepository<MoviesEntity, int>>((i) => new MovieRepository(cnstr));
builder.Services.AddScoped<IRepository<GenreEntity, int>>((i) => new GenreRepository(cnstr));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
