using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string Name;
    public string Description;

    public override string ToString()
    {
        return "Name: " + Name +
            "\nDescription" + Description;
    }
}
