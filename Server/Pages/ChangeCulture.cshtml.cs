using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Infrastructue.Middlewares;
using Microsoft.AspNetCore.Http;

namespace Server.Pages
{
	public class ChangeCultureModel : BasePageModel
	{
		public ChangeCultureModel() : base()
		{
		}

		public IActionResult OnGet(string name)
		{
			var typedHeaders =
				HttpContext.Request.GetTypedHeaders();

			var httpReferer =
				typedHeaders?.Referer?.AbsoluteUri;

			if (string.IsNullOrWhiteSpace(httpReferer))
			{
				return RedirectToPage(pageName: "/Index");
			}

			var defaultCulture = "fa";

			if (string.IsNullOrWhiteSpace(name))
			{
				name = defaultCulture;
			}

			name = name.Trim()
				.Replace(" ", string.Empty).ToLower();

			switch (name)
			{
				case "en":
				case "fa":
					{
						break;
					}
				default:
					{
						name = defaultCulture;
						break;
					}
			}

			CultureCookieHandlingMiddleware
				.SetCulture(cultureName: name);

			CultureCookieHandlingMiddleware
				.CreateCookies(httpContext: HttpContext, cultureName: name);

			return Redirect(url: httpReferer);
		}
	}
}