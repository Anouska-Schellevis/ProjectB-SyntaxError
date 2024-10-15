public class UserModel
{

    public Int64 Id { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Phone_Number { get; set; }

    public int Type { get; set; }

    public UserModel() { }

    public UserModel(Int64 id, string email, string password, string firstName, string lastName, int phoneNumber, int type)
    {
        Id = id;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;

        Phone_Number = phoneNumber;
        Type = type;

    }


}



