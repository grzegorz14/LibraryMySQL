namespace LibraryMySQL.Pages.Library;

public class BooksModel : PageModel
{
    public List<Book> Books { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string SearchTitle { get; set; } = String.Empty;

    [BindProperty(SupportsGet = true)]
    public string SearchAuthors { get; set; } = String.Empty;

    [BindProperty(SupportsGet = true)]
    public string SearchISBN { get; set; } = String.Empty;
    [BindProperty(SupportsGet = true)]
    public string BookFormat { get; set; } = String.Empty;
    public SelectList Formats { get; set; } = new SelectList("----");

    public IActionResult OnGet()
    {
        Books = MySQLConnectionManager.GetBooks();

        var formats = from book in Books
                      select book.Format;

        if (!String.IsNullOrEmpty(SearchTitle))
        {
            Books = Books.Where(b => b.Title.ToLower().Contains(SearchTitle.ToLower())).ToList();
        }
        if (!String.IsNullOrEmpty(SearchAuthors))
        {
            Books = Books.Where(b => b.Authors.ToLower().Contains(SearchAuthors.ToLower())).ToList();
        }
        if (!String.IsNullOrEmpty(SearchISBN))
        {
            Books = Books.Where(b => b.ISBN.Contains(SearchISBN)).ToList();
        }
        if (!String.IsNullOrEmpty(BookFormat))
        {
            Books = Books.Where(b => b.Format == BookFormat).ToList();
        }

        Formats = new SelectList(formats.Distinct().OrderBy(f => f).ToList());
        return Page();
    }
}
