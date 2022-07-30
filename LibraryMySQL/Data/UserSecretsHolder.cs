namespace LibraryMySQL.Data;

public class UserSecretsHolder
{
    private static UserSecrets _userSecrets = new();
    public static UserSecrets UserSecrets
    {
        get { return _userSecrets; }
        set { _userSecrets = value; }
    }
}
