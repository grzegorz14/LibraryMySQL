namespace LibraryMySQL.Pages;

public class SignUpModel : PageModel
{
    [BindProperty]
    public string Login { get; set; } = String.Empty;
    [BindProperty]
    public string Password { get; set; } = String.Empty;

    public void OnGet()
    {
        Messages.LoginInfo = "";
    }

    public IActionResult OnPost()
    {
        List<string> logins = new List<string>();

        using (var connection = MySQLConnectionManager.Connect())
        {
            if (connection is null) return RedirectToPage("./Index");

            using (var command = new MySqlCommand("SELECT Login, Password FROM users", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logins.Add(reader.GetString(0));
                    }
                }
            }

            if (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(Password))
            {
                Messages.SignUpInfo = "Login and Password fields can't be empty!";
                return RedirectToPage("./SignUp");
            }
            else if (logins.Contains(Login))
            {
                Messages.SignUpInfo = "This login is occupied. Please choose different one.";
                return RedirectToPage("./SignUp");
            }
            Messages.SignUpInfo = "";
            using (var command = new MySqlCommand($"INSERT INTO users (Login, Password) VALUES (\"{Login}\", \"{Password}\")", connection))
            {
                command.ExecuteNonQuery();
            }
        }

        Messages.SignUpSuccess = "Account created. Please log in.";
        return RedirectToPage("./Login");
    }
}
