using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Infrastructue.Middlewares;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Errors/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//app.UseMiddleware<Infrastructue.Middlewares.CultureCookieHandlingMiddleware>();
app.UseCultureCookieHandlingMiddleware();

app.MapRazorPages();
app.Run();