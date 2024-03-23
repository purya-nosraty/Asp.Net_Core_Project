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
		ViewData["Title"] = "Home";

		//throw new Exception(message: "error");
	}
}