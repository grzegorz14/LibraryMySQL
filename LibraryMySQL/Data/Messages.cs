namespace LibraryMySQL.Data;

public class Messages
{
    private static string _connectionInfo = String.Empty;
    public static string ConnectionInfo
    {
        get { return _connectionInfo; }
        set { _connectionInfo = value; }
    }

    private static string _signUpInfo = String.Empty;
    public static string SignUpInfo
    {
        get { return _signUpInfo; }
        set { _signUpInfo = value; }
    }

    private static string _signUpSuccess = String.Empty;
    public static string SignUpSuccess
    {
        get { return _signUpSuccess; }
        set { _signUpSuccess = value; }
    }

    private static string _loginInfo = String.Empty;
    public static string LoginInfo
    {
        get { return _loginInfo; }
        set { _loginInfo = value; }
    }

    public static void ClearAll()
    {
        ConnectionInfo = String.Empty;
        SignUpInfo = String.Empty;
        LoginInfo = String.Empty;
        SignUpSuccess = String.Empty;
    }
}
