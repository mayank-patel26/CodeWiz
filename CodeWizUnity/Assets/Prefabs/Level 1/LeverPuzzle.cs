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
    private bool isDone;

    private void Start()
    {
        torchCount = 0;
        torch1.SetActive(false);
        torch2.SetActive(false);
        torch3.SetActive(false);
        torch4.SetActive(false);
        isDone = true;
    }

    private void Update()
    {
        OnMouseDown();
        
        if (!isDone)
        {
            if (torchCount == 4)
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
                    if (hit.transform.gameObject.name == Switch1.name)
                    {
                        /*Debug.Log(torchCount);*/
                        toggleTorch(torch1,Switch1);
                        toggleTorch(torch3,Switch3);
                        toggleTorch(torch4,Switch4);

                    }
                    if (hit.transform.gameObject.name == Switch2.name)
                    {
                        /*Debug.Log(torchCount);*/
                        toggleTorch(torch2,Switch2);
                        toggleTorch(torch4,Switch4);

                    }
                    if (hit.transform.gameObject.name == Switch3.name)
                    {
                        /*Debug.Log(torchCount);*/
                        toggleTorch(torch2,Switch2);
                        toggleTorch(torch3,Switch3);
                        toggleTorch(torch4,Switch4);

                    }
                    if (hit.transform.gameObject.name == Switch4.name)
                    {
                        /*Debug.Log(torchCount);*/
                        toggleTorch(torch3,Switch3);
                        toggleTorch(torch4,Switch4);

                    }

                }

            }
        }
       
    }
    private void toggleTorch(GameObject torch,GameObject Switch)
    {
        if (torch.activeSelf == true)
        {
            torch.SetActive(false);
            Switch.GetComponent<Animator>().SetBool("LeverUp", false);
            torchCount--;
        }
        else
        {
            torch.SetActive(true);
            Switch.GetComponent<Animator>().SetBool("LeverUp", true);
            torchCount++;
        }
    }
}
