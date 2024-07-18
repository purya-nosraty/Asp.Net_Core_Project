namespace Server.Pages;

public class IndexModel : Infrastructure.BasePageModel
{
	public IndexModel() : base()
	{
	}

	//**********************************

	public string? FullName { get; set; }

	//**********************************

	public void OnGet()
	{
		FullName = "Pouria Nosrati";
		ViewData["PageTitle"] = Resources.PageTitles.Index;

		//throw new Exception(message: "error");
	}
}