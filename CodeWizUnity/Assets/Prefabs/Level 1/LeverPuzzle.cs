using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    public GameObject torch1;
    [SerializeField]
    private GameObject torch2;
    [SerializeField]
    private GameObject Switch1;
    private void Start()
    {
        Debug.Log(torch2.name);
    }
}
