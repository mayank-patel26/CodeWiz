using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Town : MonoBehaviour
{
    [SerializeField] Button playMaze;
    [SerializeField] GameObject player;
    [SerializeField] GameObject dialogue;
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /*if (PlayerPrefs.HasKey("playedMaze"))
            playMaze.interactable = false;
        else
            playMaze.interactable = true;*/
    }
    public void explore()
    {
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        dialogue.SetActive(false);
    }
}
