using ClientWeb.errors.Exeptions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 

//app.UseStatusCodePagesWithRedirects("/Error/?statusCode={0}");
app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");


app.UseExceptionHandlerMiddleware();
//app.UseStatusCodePagesWithRedirects("/errors/404.html");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();