using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverPuzzle : MonoBehaviour
{
    [SerializeField]
    public GameObject torch1;
    [SerializeField]
    private GameObject torch2;
    [SerializeField]
    private GameObject torch3;
    [SerializeField]
    private GameObject torch4;
    [SerializeField]
    private GameObject Switch1;
    [SerializeField]
    private GameObject Switch2;
    [SerializeField]
    private GameObject Switch3;
    [SerializeField]
    private GameObject Switch4;

    private int torchCount;
    private bool torch1Flag;
    private bool torch2Flag;
    private bool torch3Flag;
    private bool torch4Flag;
    private int t1, t2, t3, t4;
    private bool isDone;

    private void Start()
    {
        torchCount = 0;
        torch1Flag = true;
        torch2Flag = true;
        torch3Flag = true;
        torch4Flag = true;
        isDone = true;
        toggleTorch1(torch1Flag);
        torch1Flag = !torch1Flag;
        toggleTorch2(torch2Flag);
        torch2Flag = !torch2Flag;
        toggleTorch3(torch3Flag);
        torch3Flag = !torch3Flag;
        toggleTorch4(torch4Flag);
        torch4Flag = !torch4Flag;
    }

    private void Update()
    {
        OnMouseDown();
        torchCount = t1 + t2 + t3 + t4;
        if (!isDone)
        {
            if(torchCount == 4)
            {
                Debug.Log("SUCCESS");
                isDone = true;
            }
        }
    }

    private void OnMouseDown()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            isDone = false;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform)
                {
                    if(hit.transform.gameObject.name == Switch1.name)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        toggleTorch1(torch1Flag);
                        torch1Flag = !torch1Flag;

                        toggleTorch3(torch3Flag);
                        torch3Flag = !torch3Flag;

                        toggleTorch4(torch4Flag);
                        torch4Flag = !torch4Flag;

                    }
                    if (hit.transform.gameObject.name == Switch2.name)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        toggleTorch2(torch2Flag);
                        torch2Flag = !torch2Flag;

                        toggleTorch4(torch4Flag);
                        torch4Flag = !torch4Flag;
                    }
                    if (hit.transform.gameObject.name == Switch3.name)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        toggleTorch2(torch2Flag);
                        torch2Flag = !torch2Flag;

                        toggleTorch3(torch3Flag);
                        torch3Flag = !torch3Flag;

                        toggleTorch4(torch4Flag);
                        torch4Flag = !torch4Flag;
                    }
                    if (hit.transform.gameObject.name == Switch4.name)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                       
                        toggleTorch3(torch3Flag);
                        torch3Flag = !torch3Flag;

                        toggleTorch4(torch4Flag);
                        torch4Flag = !torch4Flag;
                    }

                }
            }
        }
    }

    private void toggleTorch1(bool torchflag)
    {
            if (torchflag)
            {
                for (int i = 0; i < torch1.transform.childCount; i++)
                {
                    torch1.transform.GetChild(i).gameObject.SetActive(false);
                t1 = 0;
                }
            }
            if(!torchflag)
            {
                for (int i = 0; i < torch1.transform.childCount; i++)
                {
                    torch1.transform.GetChild(i).gameObject.SetActive(true);
                t1 = 1;
                }
            }
        

    }

    private void toggleTorch2(bool torchflag)
    {
        if (torchflag)
        {
            for (int i = 0; i < torch2.transform.childCount; i++)
            {
                torch2.transform.GetChild(i).gameObject.SetActive(false);
                t2 = 0;
            }
        }
        if (!torchflag)
        {
            for (int i = 0; i < torch2.transform.childCount; i++)
            {
                torch2.transform.GetChild(i).gameObject.SetActive(true);
                t2 = 1;
            }
        }
        

    }

    private void toggleTorch3(bool torchflag)
    {
        if (torchflag)
        {
            for (int i = 0; i < torch3.transform.childCount; i++)
            {
                torch3.transform.GetChild(i).gameObject.SetActive(false);
                t3 = 0;
            }
        }
        if (!torchflag)
        {
            for (int i = 0; i < torch3.transform.childCount; i++)
            {
                torch3.transform.GetChild(i).gameObject.SetActive(true);
                t3 = 1;
            }
        }
        

    }

    private void toggleTorch4(bool torchflag)
    {
        if (torchflag)
        {
            for (int i = 0; i < torch4.transform.childCount; i++)
            {
                torch4.transform.GetChild(i).gameObject.SetActive(false);
                t4 = 0;
            }
        }
        if (!torchflag)
        {
            for (int i = 0; i < torch4.transform.childCount; i++)
            {
                torch4.transform.GetChild(i).gameObject.SetActive(true);
                t4 = 1;
            }
        }
        

    }

}
