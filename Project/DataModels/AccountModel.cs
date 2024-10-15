public class AccountModel
{

    public Int64 Id { get; set; }
    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public AccountModel(Int64 id, string email, string password, string fullname)
    {
        Id = id;
        EmailAddress = email;
        Password = password;
        FullName = fullname;
    }


}



