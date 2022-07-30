namespace LibraryMySQL.Pages.Library;

public class EditModel : PageModel
{
    [BindProperty]
    public Book Book { get; set; }

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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            string update = @$"UPDATE books
                               SET Authors=""{Book.Authors}"", Title=""{Book.Title}"", ReleaseDate=""{Book.ReleaseDate.ToString("yyyy-MM-dd hh:mm:ss")}"", ISBN=""{Book.ISBN}"", Format=""{Book.Format}"", Pages=""{Book.Pages}"", Description=""{Book.Description}""
                               WHERE Id={Book.Id}";

            using (var connection = MySQLConnectionManager.Connect())
            {
                if (connection is null) return RedirectToPage("./../Index");
                using (var command = new MySqlCommand(update, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            if (!MySQLConnectionManager.BookExists(Book.Id))
            {
                return NotFound();
            }
            else
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        return RedirectToPage("./Books");
    }
}
