namespace LibraryMySQL.Pages.Library;

public class DeleteModel : PageModel
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

    public IActionResult OnPost(int? id)
    {
        if (id is null || Book is null)
        {
            return NotFound();
        }

        using (var connection = MySQLConnectionManager.Connect())
        {
            if (connection is null) return RedirectToPage("./../Index");
            using (var command = new MySqlCommand($"DELETE FROM books WHERE Id={id}", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        return RedirectToPage("./Books");
    }
}