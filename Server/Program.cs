using System.Globalization;
using Infrastructue.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRazorPages();

builder.Services.Configure<RequestLocalizationOptions>(option =>
{
	var supportedCultures = new[]
	{
		new CultureInfo(name: "fa-IR"),
		new CultureInfo(name: "en-US"),
	};

	option.SupportedCultures = supportedCultures;
	option.SupportedUICultures = supportedCultures;

	option.DefaultRequestCulture =
		new RequestCulture(culture: "en-US", uiCulture: "en-US");
});

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
app.UseCultureCookie();
app.UseGlobalException();

app.MapRazorPages();
await app.RunAsync();