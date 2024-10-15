using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class UserLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static UserModel? CurrentAccount { get; private set; }

    public UserLogic()
    {
        // Could do something here

    }

    public UserModel GetById(int id)
    {
        return UserAccess.GetById(id);
    }

    public UserModel GetByType(string email)
    {
        return UserAccess.GetByType(email);
    }

    public void CreateAccount(UserModel user)
    {
        UserAccess.Write(user);
    }

    public UserModel CheckLogin(string email, string password)
    {

        UserModel acc = UserAccess.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            Console.WriteLine("Account login succesful");
            return acc;
        }
        return null;
    }
}




