using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    
    private void OnCollisionEnter(Collision collision)
    {
        dialogueBox.SetActive(true);
        GameObject.Find("Main Player").SetActive(false);
    }
}
