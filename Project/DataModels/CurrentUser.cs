public class UserSession
{
    private static UserSession _instance;
 
    public static UserSession Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UserSession();
            }
            return _instance;
        }
    }
 
    public UserModel CurrentUser { get; set; }
 
    private UserSession() 
    {
        CurrentUser = null;
    }
}