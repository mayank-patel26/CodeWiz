using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //no of rings, from shlok's formula
    int n = 3;

    [SerializeField]
    GameObject[] rings;

    //1 unit rotation = 30 degree clockwise
    //rotation speed, degrees per second
    private float rotationSpeed = 15.0f;
    private float animSpeed = 0.01f;

    //random generation
    private float correctRotation, guessedRotation;

    //no of rotations is timeLeft, n is number of seconds for which the object will rotate
    private float timeLeft;

    //user guess
    private float guess;

    private int i;

    //capture numeric input for testing
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    //flag
    private bool done = true;
    private bool completed = false;


    void Start()
    {

        Debug.Log(Login.currentStudent.fullname);
        i = n - 1;

        //disabling all rings
        disable();

        //random generation of answer
        correctRotation = Random.Range(1, 10);
        timeLeft = Mathf.Round(correctRotation);
        Debug.Log(timeLeft);

        //enabling only the required rings, as per value of n
        for (int i = 0; i < n; i++)
        {
            rings[i].SetActive(true);
        }
    }


    void Update()
    {
        //countdown
        if (Input.GetKeyDown(KeyCode.Space))
        {
            done = false;
        }

        if (done == false && completed == false)
        {
            timeLeft -= 0.01f;
            if (timeLeft > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    //nth ring will rotate at speed of n x rotationSpeed 
                    rings[i].transform.Rotate(Vector3.back, (Mathf.Pow(rotationSpeed, (i + 1))) * 0.01f);
                    /*Debug.Log(rotationSpeed * (i + 1));*/
                }
            }
            else
            {
                done = true;
            }
        }

        if (done == true)
        {
            if (completed == true)
            {
                guess -= animSpeed;
                if (guess > 0)
                {
                    if (i >= 0)
                    {
                        rings[i].transform.Rotate(Vector3.forward, (Mathf.Pow(rotationSpeed, (i + 1))) * animSpeed);
                    }
                }
                else
                {
                    i--;
                    guess = guessedRotation;
                }
            }
            else
            {
                guessAnswer();
            }
        }
    }

    private void guessAnswer()
    {
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int numberPressed = (i + 1);
                guessedRotation = guess = Mathf.Round(numberPressed);
                Debug.Log(guess);
                completed = true;
                break;
            }
        }
    }

    void disable()
    {
        //disable all rings
        for (int i = 0; i < 3; i++)
        {
            rings[i].SetActive(false);
        }
    }
}
