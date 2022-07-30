namespace LibraryMySQL.Models;

public static class MySQLConnectionManager
{
    public static MySqlConnection? Connect()
    {
        MySqlConnection connection = new MySqlConnection(UserSecretsHolder.UserSecrets.GetUserSecrets());

        try
        {
            connection.Open();
            //Console.WriteLine("Connected");
            Messages.ConnectionInfo = "";
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Messages.ConnectionInfo = ex.Message;
        }
        return null;
    }

    public static void CreateTables(MySqlConnection connection)
    {
        using (MySqlCommand command = connection.CreateCommand())
        {
            command.CommandText = @"CREATE TABLE IF NOT EXISTS books (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Authors VARCHAR(255),
                    Title VARCHAR(255),
                    ReleaseDate DATETIME,
                    ISBN CHAR(20),
                    Format CHAR(4),
                    Pages INT,
                    Description VARCHAR(255)
                )";
            command.ExecuteNonQuery();

            command.CommandText = "SELECT COUNT(*) from books";
            if (command.ExecuteScalar().ToString() == "0")
            {
                SeedDataToBooks(command);
            }

            command.CommandText = @"CREATE TABLE IF NOT EXISTS users (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Login VARCHAR(255),
                    Password VARCHAR(255)
                )";
            command.ExecuteNonQuery();
        }
    }

    private static void SeedDataToBooks(MySqlCommand command)
    {
        command.CommandText = $"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"Dune\", \"Frank Herbert\", \"1965-08-17\", \"2534867891011\", \"mobi\", \"412\", \"Cool sci-fi book\")";
        command.ExecuteNonQuery();
        command.CommandText = $"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"Harry Potter and the Philosopher's Stone\", \"J. K. Rowling\", \"1997-06-26\", \"9780590353427\", \"pdf\", \"320\", \"Must read for every kid\")";
        command.ExecuteNonQuery();
        command.CommandText = $"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"Sword of Destiny\", \"Andrzej Sapkowski\", \"1992-02-14\", \"9780316389709\", \"epub\", \"384\", \"Book based on PC Witcher games\")";
        command.ExecuteNonQuery();
        command.CommandText = $"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"A Brief History of Time\", \"Stephen Hawking\", \"1988-11-07\", \"7432867891051\", \"pfd\", \"256\", \"For real space enjoyers\")";
        command.ExecuteNonQuery();
        command.CommandText = $"INSERT INTO books (Title, Authors, ReleaseDate, ISBN, Format, Pages, Description) VALUES(\"The Winds of Winter\", \"George R. R. Martin\", \"2137-12-12\", \"4492969841954\", \"iba\", \"999\", \"Waiting 11 years...\")";
        command.ExecuteNonQuery();
    }

    public static List<Book> GetBooks()
    {
        List<Book> books = new List<Book>();

        using (var connection = Connect())
        {
            using (var command = new MySqlCommand("SELECT Authors, Title, ReleaseDate, ISBN, Format, Pages, Description, Id FROM books", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(new Book()
                        {
                            Id = reader.GetInt32(7),
                            Authors = reader.GetString(0),
                            Title = reader.GetString(1),
                            ReleaseDate = reader.GetDateTime(2),
                            ISBN = reader.GetString(3),
                            Format = reader.GetString(4),
                            Pages = reader.GetInt32(5),
                            Description = reader.GetString(6)
                        });
                    }
                }
            }
        }
        return books;
    }

    public static bool BookExists(int id)
    {
        return GetBooks().Any(b => b.Id == id);
    }
}
