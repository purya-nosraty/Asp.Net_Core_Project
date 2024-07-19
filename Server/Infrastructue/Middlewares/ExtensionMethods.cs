using System;
using Microsoft.AspNetCore.Builder;

namespace Infrastructue.Middlewares
{
	public static class ExtensionMethods : Object
	{
		static ExtensionMethods()
		{
		}

		public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
		{
			return app.UseMiddleware<CultureCookieHandlerMiddleware>();
		}
	}
}