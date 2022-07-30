namespace LibraryMySQL.Models;

public class UserSecrets
{
    public string Server { get; set; } = "localhost";
    public uint Port { get; set; }
    public string Database {  get; set; } = String.Empty;
    public string User { get; set; } = "root";
    public string Password { get; set; } = String.Empty;

    public string GetUserSecrets() => $"Server={Server};Port={Port};Database={Database};User={User};Password={Password}";
}
