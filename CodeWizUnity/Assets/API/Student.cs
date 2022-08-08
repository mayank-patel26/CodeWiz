using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Student
{
    public string _id;
    public string username;
    public string fullname;
    public string password;
    public string email;
    public List<Level> level;
    public List<string> badges;
    public int __v;

    public override string ToString()
    {
        return "ID: " + _id +
            "\nEmail: " + email +
            "\nFullname: " + fullname + 
            "\nLevel: " + string.Join("\n", (List<Level>)level);
    }
}




