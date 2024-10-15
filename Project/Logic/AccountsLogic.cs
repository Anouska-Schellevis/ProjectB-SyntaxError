using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    public static AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        // Could do something here

    }

    public AccountModel GetById(int id)
    {
        return AccountsAccess.GetById(id);
    }

    public AccountModel CheckLogin(string email, string password)
    {

        AccountModel acc = AccountsAccess.GetByEmail(email);
        if (acc != null && acc.Password == password)
        {
            CurrentAccount = acc;
            return acc;
        }
        return null;
    }
}




