using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model of the User data object that can be used to communicate with the server
/// </summary>
[Serializable]
public class User
{
    public long id;
    public string firstName;
    public string lastName;
    public string username;
    public string email;
    public string password;
    public string createdAt;
    public string updatedAt;
}
