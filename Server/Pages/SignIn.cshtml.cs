namespace Server.Pages;

public class SignIn : Infrastructure.BasePageModel
{
	public SignIn() : base()
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