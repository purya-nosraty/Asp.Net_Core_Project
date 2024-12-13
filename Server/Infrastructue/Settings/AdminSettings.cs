namespace Server.Infrastructue.Settings;

public class AdminSettings
{
	public static readonly string KeyName = nameof(AdminSettings);

	public AdminSettings() : base()
	{
	}

	public int Age { get; set; }

	public string? Name { get; set; }

	public string? NationalCode { get; set; }
}