using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder =
	WebApplication.CreateBuilder();

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
app.MapRazorPages();
app.Run();