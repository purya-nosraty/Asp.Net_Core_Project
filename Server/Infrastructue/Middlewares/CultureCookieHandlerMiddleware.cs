using System;
using System.Linq;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace Infrastructue.Middlewares;

public class CultureCookieHandlerMiddleware
{
	#region StaticMembers
	private readonly static string CookieName = "Culture.Cookie";

	public static void SetCulture(string? cultureName)
	{
		if (!string.IsNullOrWhiteSpace(cultureName))
		{
			var cultureInfo =
				new CultureInfo(name: cultureName);

			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}
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

		if (!string.IsNullOrWhiteSpace(cultureName))
		{
			cultureName =
				cultureName.Substring(startIndex: 0, length: 2).ToLower();

			httpContext.Response.Cookies
				.Append(key: CookieName, value: cultureName, options: cookieOptions);
		}
	}

	public static string? GetCultureNameByCookie
		(HttpContext httpContext, List<string>? supportedCultures)
	{
		if (supportedCultures == null || supportedCultures.Count == 0)
		{
			return null;
		}

		var cultureName =
			httpContext.Request.Cookies[key: CookieName];

		if (string.IsNullOrWhiteSpace(cultureName) ||
			!supportedCultures.Contains(cultureName))
		{
			return null;
		}

		return cultureName;
	}
	#endregion /StaticMembers

	#region Contructor
	public CultureCookieHandlerMiddleware
		(RequestDelegate next,
		IOptions<RequestLocalizationOptions>? requestLocalizationOptions) : base()
	{
		Next = next;
		RequestLocalizationOptions = requestLocalizationOptions?.Value;
	}
	#endregion /Contructor

	#region Properties
	private RequestDelegate Next { get; }
	private RequestLocalizationOptions? RequestLocalizationOptions { get; }
	#endregion /Properties

	public async Task InvokeAsync(HttpContext httpContext)
	{
		var defaultCultureName =
			RequestLocalizationOptions?
			.DefaultRequestCulture.UICulture.Name;

		var supportedCultures =
			RequestLocalizationOptions?.SupportedCultures?
			.Select(current => current.Name)
			.ToList();

		var currentCultureName =
			GetCultureNameByCookie(httpContext, supportedCultures);

		if (string.IsNullOrWhiteSpace(currentCultureName))
		{
			currentCultureName = defaultCultureName;
		}

		SetCulture(cultureName: currentCultureName);

		await Next(context: httpContext);
	}
}