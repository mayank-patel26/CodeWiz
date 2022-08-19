using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    [SerializeField]
    public long[][] time;
    public int[] score;
    [SerializeField]
    public int[][] incat;
    public string badges;
    public bool helpReq;
    public string[] mentorUser;
}
