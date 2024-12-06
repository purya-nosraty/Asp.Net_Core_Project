using System;
using Microsoft.AspNetCore.Builder;

namespace Infrastructue.Middlewares;

public static class ExtensionMethods
{
	static ExtensionMethods()
	{
	}

	public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
	{
		return app.UseMiddleware<CultureCookieHandlerMiddleware>();
	}

	public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
	{
		return app.UseMiddleware<GlobalExceptionHandelrMiddleware>();
	}
}