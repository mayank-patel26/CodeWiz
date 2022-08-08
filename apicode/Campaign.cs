using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Campaign
{
    public string Name;
    public string[] Players;

    public override string ToString()
    {
        return "----------------------" +
            "\nName: " + Name +
            "\nPlayers: " + string.Join("",(object[])Players);
    }
}
