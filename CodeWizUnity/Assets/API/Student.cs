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
    [SerializeField]
    public Level[] level;
    public int __v;

    public override string ToString()
    {
        return _id;
    }
}




