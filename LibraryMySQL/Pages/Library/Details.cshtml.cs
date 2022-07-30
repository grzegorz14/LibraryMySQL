namespace LibraryMySQL.Pages.Library;

public class DetailsModel : PageModel
{
    [BindProperty]
    public Book Book { get; set; } = new();

    public IActionResult OnGet(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        Book = MySQLConnectionManager.GetBooks().First(b => b.Id == id);

        if (Book is null)
        {
            return NotFound();
        }
        return Page();
    }
}
