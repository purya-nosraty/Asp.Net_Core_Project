using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructue.Middlewares;
public class GlobalExceptionHandelrMiddleware
{
	public GlobalExceptionHandelrMiddleware(RequestDelegate next) : base()
	{
		Next = next;
	}

	private RequestDelegate Next { get; }

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await Next(httpContext);
		}
		catch (Exception)
		{
			httpContext.Response.Redirect
				(location: "/Errors/Error", permanent: false);
		}
	}
}