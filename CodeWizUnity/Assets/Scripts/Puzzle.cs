using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    int levelNumber = 0;
    //no of rings, from shlok's formula
    int n;

    [SerializeField]
    GameObject[] rings;
    [SerializeField]
    GameObject gameCanvas;
    [SerializeField]
    GameObject successCanvas;

    //1 unit rotation = 30 degree clockwise
    //rotation speed, degrees per second
    private float rotationSpeed = 30.0f;
    private float animSpeed = 0.01f;

    //random generation
    private float correctRotation, guessedRotation;

    //no of rotations is timeLeft, n is number of seconds for which the object will rotate
    private float timeLeft;

    //user guess
    private float guess;

    private int ringNo;


    //flag
    private bool done = false;
    private bool completed = false;
    private bool CFlag = false;
    private bool ACFlag = false;
    private bool rotateACFlag = false;
    private int numberPressed;

    private Quaternion startRotation;

    void Start()
    {
        startGame();
    }
    void startGame()
    {
        //n = DynamicDifficulty.getinitialN(levelNumber)+1;
        n = 3;
        //Debug.Log(Login.currentStudent.fullname);
        ringNo = n - 1;
        
        //disabling all rings
        disable();

        for (int i = 0; i < n; i++)
        {
            rings[i].SetActive(true);
            startRotation = rings[i].transform.rotation;
        }
        //random generation of answer
        
        /*if (done == true)
        {
            //correctRotation = 2;
            
        }*/
    }

    void Update()
    {
/*
        if (done == false && completed == false)
        {
            //Debug.Log(n);
            timeLeft -= 0.01f;
            if (timeLeft > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    //nth ring will rotate at speed of n x rotationSpeed 
                    rings[i].transform.Rotate(Vector3.back, (Mathf.Pow(rotationSpeed, (i + 1))) * 0.01f);
                    *//*Debug.Log(rotationSpeed * (i + 1));*//*
                }
            }
            else
            {
                done = true;
            }
        }*/

        if (done == true || CFlag == true)
        {
            if (completed == true || CFlag == true)
            {
                guess -= animSpeed;
                if (guess > 0)
                {
                    rings[ringNo].transform.Rotate(Vector3.forward, (Mathf.Pow(rotationSpeed, 1)) * animSpeed);
                }

            }
        }
        if (rotateACFlag == true)
        {
            if (completed == true || ACFlag == true)
            {
                guess -= animSpeed;
                if (guess > 0)
                {
                    rings[ringNo].transform.Rotate(Vector3.back, (Mathf.Pow(rotationSpeed, 1)) * animSpeed);
                }

            }
        }
    }
    bool isRotating = false;
    public TMP_InputField answerInputField;
    public void guessAnswer()
    {
        Debug.Log("Guessed");
        numberPressed = int.Parse(answerInputField.text);
       /* guessedRotation = guess = Mathf.Round(numberPressed);
        Debug.Log(guess);*/
        completed = true;
        StartCoroutine(rotate3(numberPressed));

    }

    public void rotateAC()
    {
        CFlag = false;
        rotateACFlag = true;
        Debug.Log("AC Rotate");
        ACFlag = true;
        StartCoroutine(rotate3(1));
    }

    public void rotateC()
    {
        ACFlag = false;
        rotateACFlag = false;
        Debug.Log("C Rotate");
        CFlag = true;
        StartCoroutine(rotate3(1));
    }
    IEnumerator rotate3(int x)
    {
        //animSpeed = 1f;
        for(int i=0;i<x;i++)
        {
            guess = 1;
            ringNo = 0;
            yield return new WaitForSeconds(1);
            for (int j=0;j<x;j++)
            {
                guess = 1;
                ringNo = 1;
                yield return new WaitForSeconds(1);
                for (int k=0;k<x;k++)
                {
                    guess = 1;
                    ringNo = 2;
                    yield return new WaitForSeconds(1);
                }
            }
        }
        CFlag = true;
        ACFlag = true;
        if(done == true && numberPressed == correctRotation)
        {
            successCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    /*void rotate(int guess,int i,int animSpeed)
    {
        
        
    }*/
    void disable()
    {
        //disable all rings
        for (int i = 0; i < 3; i++)
        {
            rings[i].SetActive(false);
        }
    }

    public void onPlayClick()
    {
        done = true;
        for (int i = 0; i < n; i++)
        {
            rings[i].transform.rotation = startRotation;
        }
        playGame();
    }

    public void playGame()
    {
        //correctRotation = Random.Range(1, 12);
        correctRotation = 2;
        timeLeft = Mathf.Round(correctRotation);
        Debug.Log(correctRotation);
        //Debug.Log(timeLeft);

        //enabling only the required rings, as per value of n
        for (int i = 0; i < n; i++)
        {
            rings[i].transform.Rotate(Vector3.back, rotationSpeed * Mathf.Pow(correctRotation, i + 1));
        }
    }

}
