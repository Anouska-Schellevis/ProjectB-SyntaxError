public class UserSession
{
    private static UserSession _instance;

    public static UserSession Instance => _instance ??= new UserSession();

    public UserModel CurrentUser { get; set; }

    private UserSession() { } // Private constructor to prevent instantiation
}
