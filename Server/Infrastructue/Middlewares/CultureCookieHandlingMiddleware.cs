using System;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Infrastructue.Middlewares;

public class CultureCookieHandlingMiddleware : object
{
	#region StaticMembers
	private readonly static string CookieName = "Culture.Cookie";

	public static void SetCulture(string cultureName)
	{
		var cultureInfo =
			new CultureInfo(name: cultureName);

		Thread.CurrentThread.CurrentCulture = cultureInfo;
		Thread.CurrentThread.CurrentUICulture = cultureInfo;
	}

	public static void CreateCookies(HttpContext httpContext, string cultureName)
	{
		var cookieOptions = new CookieOptions
		{
			Path = "/",
			MaxAge = null,
			Secure = false,
			HttpOnly = false,
			IsEssential = false,
			SameSite = SameSiteMode.Unspecified,
			Expires = DateTimeOffset.UtcNow.AddYears(1),
		};

		httpContext.Response.Cookies.Delete(key: CookieName);

		if (string.IsNullOrWhiteSpace(cultureName) == false)
		{
			cultureName =
				cultureName.Substring(startIndex: 0, length: 2).ToLower();

			httpContext.Response.Cookies
				.Append(key: CookieName, value: cultureName, options: cookieOptions);
		}
	}
	#endregion /StaticMembers

	public CultureCookieHandlingMiddleware(RequestDelegate next) : base()
	{
		Next = next;
	}

	private RequestDelegate Next { get; }

	public async Task InvokeAsync(HttpContext httpContext)
	{
		var cultureName =
			httpContext.Request.Cookies[key: CookieName]?
			.Substring(startIndex: 0, length: 2).ToLower();

		switch (cultureName)
		{
			case "fa":
			case "en":
				{
					SetCulture(cultureName);
					break;
				}

			default:
				{
					SetCulture(cultureName: "fa");
					break;
				}
		}

		await Next(context: httpContext);
	}
}