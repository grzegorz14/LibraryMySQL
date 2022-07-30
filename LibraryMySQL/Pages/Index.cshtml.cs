namespace LibraryMySQL.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    [BindProperty]
    public UserSecrets UserSecrets { get; set; }


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Messages.ClearAll();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        UserSecretsHolder.UserSecrets = UserSecrets;

        return RedirectToPage("./Login");
    }
}
