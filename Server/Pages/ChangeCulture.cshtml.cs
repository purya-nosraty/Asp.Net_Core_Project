using System.Linq;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infrastructue.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace Server.Pages;

public class ChangeCultureModel : BasePageModel
{
	public ChangeCultureModel
		(IOptions<RequestLocalizationOptions>? requestLocalizationOptions) : base()
	{
		RequestLocalizationOptions =
			requestLocalizationOptions?.Value;
	}

	private RequestLocalizationOptions? RequestLocalizationOptions { get; }

	public IActionResult OnGet(string? cultureName)
	{
		var typedHeaders =
			HttpContext.Request.GetTypedHeaders();

		var httpReferer =
			typedHeaders?.Referer?.AbsoluteUri;

		if (string.IsNullOrWhiteSpace(httpReferer))
		{
			return RedirectToPage(pageName: "/Index");
		}

		var defaultCultureName =
			RequestLocalizationOptions?
			.DefaultRequestCulture.UICulture.Name;

		var supportedCultureNames =
			RequestLocalizationOptions?.SupportedUICultures?
			.Select(current => current.Name)
			.ToList();

		if (string.IsNullOrWhiteSpace(cultureName))
		{
			cultureName = defaultCultureName;
		}

		if (supportedCultureNames?.Contains(item: cultureName!) == false)
		{
			cultureName = defaultCultureName;
		}

		CultureCookieHandlerMiddleware.SetCulture(cultureName);
		CultureCookieHandlerMiddleware.CreateCookies(httpContext: HttpContext, cultureName!);

		return Redirect(url: httpReferer);
	}
}