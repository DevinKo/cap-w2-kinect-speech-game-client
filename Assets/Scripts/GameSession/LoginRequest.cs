using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LoginRequest
{
    public string email;
    public string password;

    public LoginRequest(string username, string password)
    {
        this.email = username;
        this.password = password;
    }
}
