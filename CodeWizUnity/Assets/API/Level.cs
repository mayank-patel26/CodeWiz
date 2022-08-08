using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public List<List<int>> time;
    public List<int> score;
    public List<List<int>> incat;
    public List<string> badges;
    public bool helpReq;
    public object mentorUser;
}
