using Microsoft.Extensions.Options;
using Server.Infrastructue.Settings;

namespace Server.Pages
{
	public class ContactModel : Infrastructure.BasePageModel
	{
		public ContactModel(IOptions<AdminSettings> adminSettings)
		{
			AdminSettings = adminSettings.Value;
		}

		public AdminSettings AdminSettings { get; }
	}
}
