using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Puzzle : MonoBehaviour
{
    int levelNumber = 1;
    //no of rings, from shlok's formula
    int n;

    [SerializeField]
    GameObject[] rings;
    [SerializeField]
    GameObject gameCanvas;
    [SerializeField]
    GameObject successCanvas;
    [SerializeField] Camera cam;
    [SerializeField] Transform loopSelectionObject;
    [SerializeField] GameObject failureCanvas;


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
/*    private bool done = false;*/
    private bool completed = false;
    private bool CFlag = false;
    private bool ACFlag = true;
    private int currentRing=-1;
    private int numberPressed;
    private Quaternion startRotation;

    void Start()
    {
        APIConnections.FetchLevel(levelNumber);
        DynamicDifficulty.getinitialN();
        startRotation = rings[0].transform.rotation;
        //layer = LayerMask.GetMask("Rings");
        startGame();
    }
    void startGame()
    {
        failureCanvas.SetActive(false);
        DynamicDifficulty.startTimer();
        n = DynamicDifficulty.currentDifficulty+1;
        //n = 3;
        //Debug.Log(Login.currentStudent.fullname);
        //ringNo = n - 1;

        //disabling all rings
        //disable();
        correctRotation = Random.Range(1, 3);
        int i;
        for (i = 0; i < n; i++)
        {
            rings[i].SetActive(true);
            //startRotation = rings[i].transform.rotation;
            rings[i].transform.Rotate(Vector3.back, rotationSpeed * Mathf.Pow(correctRotation, i + 1));
        }
        //random generation of answer
        rings[0].transform.parent.GetChild(3).GetChild(n-1).gameObject.SetActive(true);
        
        /*if (done == true)
        {
            correctRotation = 2;
            
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
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000f))
            {
                if (hit.collider.CompareTag("Rings"))
                {
                    Transform objectHit = hit.transform;
                    name = objectHit.name;
                    //Debug.Log(name);
                    if(currentRing!=-1)
                        loopSelectionObject.GetChild(currentRing).gameObject.SetActive(false);
                    currentRing = int.Parse(name[name.Length - 1] + "") - 1;
                    loopSelectionObject.GetChild(currentRing).gameObject.SetActive(true);
                }
            }
            else
            {
                loopSelectionObject.GetChild(0).gameObject.SetActive(false);
                loopSelectionObject.GetChild(1).gameObject.SetActive(false);
                loopSelectionObject.GetChild(2).gameObject.SetActive(false);
            }
        }
        if (completed == true || (CFlag == true && ACFlag == false))
        {
            guess -= animSpeed;
            if (guess > 0)
            {
                rings[ringNo].transform.Rotate(Vector3.forward, (Mathf.Pow(rotationSpeed, 1)) * animSpeed);
            }
            else
            {
                CFlag = false;
            }
        }

        if ((ACFlag == true && CFlag == false) || completed == true)
        {
            guess -= animSpeed;
            if (guess > 0)
            {
                rings[ringNo].transform.Rotate(Vector3.back, (Mathf.Pow(rotationSpeed, 1)) * animSpeed);
            }
            else
            {
                ACFlag = false;
            }
        }

    }
    public void rotateClockwise()
    {
        if(currentRing==-1)
            return;
        //Debug.Log(currentRing);
        guess = 1;
        CFlag = true;
        ringNo = 2-currentRing;
    }
    public void rotateAntiClockwise()
    {
        if (currentRing == -1)
            return;
        //Debug.Log(currentRing);
        guess = 1;
        ACFlag = true;
        ringNo = 2-currentRing;
    }
    public void reset()
    {
        for (int i = 0; i < n; i++)
        {
            rings[i].transform.localRotation = startRotation;
            
            //startRotation = rings[i].transform.rotation;
            rings[i].transform.Rotate(Vector3.back, rotationSpeed * Mathf.Pow(correctRotation, i + 1));
        }

    }
    public TMP_InputField answerInputField;
    private long timeElapsed;
    public void guessAnswer()
    {
        if (answerInputField.text.Equals(""))
            return;
        timeElapsed = DynamicDifficulty.getTimeElapsed();
        numberPressed = int.Parse(answerInputField.text);
        completed = true;
        reset();
        if (n == 3)
            StartCoroutine(rotate3(numberPressed));
        else if (n == 2)
            StartCoroutine(rotate2(numberPressed));
        else if (n == 1)
            StartCoroutine(rotate1(numberPressed));

    }
    IEnumerator rotate3(int x)
    {
         //animSpeed = 1f;
        for (int i = 0; i < x; i++)
        {
            guess = 1;
            ringNo = 0;
            yield return new WaitForSeconds(1);
            for (int j = 0; j < x; j++)
            {
                guess = 1;
                ringNo = 1;
                yield return new WaitForSeconds(1);
                for (int k = 0; k < x; k++)
                {
                    guess = 1;
                    ringNo = 2;
                    yield return new WaitForSeconds(1);
                }
            }
        }
        completed = false;
        complete(x);
            
        /*if (done == true && numberPressed == correctRotation)
        {
            successCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }*/
    }
    IEnumerator rotate2(int x)
    {
        //animSpeed = 1f;
        for (int i = 0; i < x; i++)
        {
            guess = 1;
            ringNo = 1;
            yield return new WaitForSeconds(1);
            for (int j = 0; j < x; j++)
            {
                guess = 1;
                ringNo = 2;
                yield return new WaitForSeconds(1);
            }
        }
        completed = false;
        complete(x);
    }
    IEnumerator rotate1(int x)
    {
        //animSpeed = 1f;
        for (int i = 0; i < x; i++)
        {
            guess = 1;
            ringNo = 2;
            yield return new WaitForSeconds(1);
        }
        completed = false;
        complete(x);
    }
    void complete(int x)
    {
        if (x == correctRotation)
        {
            DynamicDifficulty.NextDifficulty(timeElapsed, n, 0);
            APIConnections.makeLevelChanges(levelNumber, n, timeElapsed, levelNumber, 0);
            APIConnections.UpdateLevel(levelNumber);
            APIConnections.FetchLevel(levelNumber);
            if (DynamicDifficulty.currentDifficulty < 3)
                startGame();
            else
                successCanvas.SetActive(true);
        }
        else
            failureCanvas.SetActive(true);
    }
        /*    public void onPlayClick()
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
                //Debug.Log(timeLeft);

                //enabling only the required rings, as per value of n
                for (int i = 0; i < n; i++)
                {
                    rings[i].transform.Rotate(Vector3.back, rotationSpeed * Mathf.Pow(correctRotation, i + 1));
                }
            }*/

    }
