namespace LibraryMySQL.Pages.Library;

public class CreateModel : PageModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Book Book { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        using (var connection = MySQLConnectionManager.Connect()) //przenieœæ do mysqlconnectionManager
        {
            if (connection is null) return RedirectToPage("./../Index");

            using (var command = new MySqlCommand($"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"{Book.Title}\", \"{Book.Authors}\", \"{Book.ReleaseDate.ToString("yyyy-MM-dd hh:mm:ss")}\", \"{Book.ISBN}\", \"{Book.Format}\", \"{Book.Pages}\", \"{Book.Description}\")", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        return RedirectToPage("./Books");
    }
}
