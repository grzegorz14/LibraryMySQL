namespace LibraryMySQL.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Login { get; set; } = String.Empty;
    [BindProperty]
    public string Password { get; set; } = String.Empty;

    public IActionResult OnGet()
    {
        using (var connection = MySQLConnectionManager.Connect())
        {
            if (connection is null) return RedirectToPage("./Index");

            MySQLConnectionManager.CreateTables(connection);
        }

        return Page();
    }

    public IActionResult OnPost()
    {
        List<(string, string)> users = new List<(string, string)>();

        using (var connection = MySQLConnectionManager.Connect())
        {
            if (connection is null) return RedirectToPage("./Index");

            using (var command = new MySqlCommand("SELECT Login, Password FROM users", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add((reader.GetString(0), reader.GetString(1)));
                    }
                }
            }

            if (users.Contains((Login, Password)))
            {
                Messages.LoginInfo = "";
                return RedirectToPage("./Library/Books");
            }
            else
            {
                Messages.LoginInfo = "Incorrect login or password";
                return RedirectToPage("./Login");
            }
        }
    }
}
