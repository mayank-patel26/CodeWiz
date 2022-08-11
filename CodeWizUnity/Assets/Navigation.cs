using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour {
    
    // Start is called before the first frame update
    public void Forest(){
        SceneManager.LoadScene("Forest");
    }
    public void Town(){
        SceneManager.LoadScene("Town");
    }
    public void Mountain(){
        SceneManager.LoadScene("GraphColouring");
    }
    public void BackHome(){
        SceneManager.LoadScene("Main Menu");
    }
    public void Library(){
        SceneManager.LoadScene("Level_1");
    }
}

