using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string _id;
    public string email;
    public string password;
    [SerializeField]
    public Character[] characters;
    [SerializeField]
    public Campaign[] games;

    public override string ToString()
    {
        return "ID: " + _id +
            "\nEmail: " + email +
            "\nCharacters: " + string.Join("", (object[])characters) +
            "\nGames: " + string.Join("", (object[])games);
    }
}
