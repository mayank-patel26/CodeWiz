using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour {
    
    // Start is called before the first frame update
    public void Forest(){
        SceneManager.LoadScene(0);
    }
    public void Town(){
        SceneManager.LoadScene(2);
    }
    public void Mountain(){
        SceneManager.LoadScene(4);
    }
    public void BackHome(){
        SceneManager.LoadScene(5);
    }
}

