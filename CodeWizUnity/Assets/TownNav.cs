using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownNav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHomeClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void onContinueClick()
    {
        SceneManager.LoadScene("Forest");
    }
}
