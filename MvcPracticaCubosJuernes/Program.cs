using Microsoft.EntityFrameworkCore;
using MvcPracticaCubosJuernes.Data;
using MvcPracticaCubosJuernes.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configurar DbContext con MySQL
string connectionString = builder.Configuration.GetConnectionString("MySql")!;
builder.Services.AddDbContext<CubosContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registrar Repositorio
builder.Services.AddTransient<RepositoryCubos>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Habilitar Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Cubos}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
