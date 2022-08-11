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
    [SerializeField] Animator doorAnimator;

    [SerializeField] GameObject cutscene;
    [SerializeField] GameObject successM;

    private int torchCount;
    private bool isDone;

    #region SwitchAnims
    private Animator SAnim1;
    private Animator SAnim2;
    private Animator SAnim3;
    private Animator SAnim4;
    #endregion

    private void Start()
    {
        SAnim1 = Switch1.transform.GetChild(1).gameObject.GetComponent<Animator>();
        SAnim2 = Switch2.transform.GetChild(1).gameObject.GetComponent<Animator>();
        SAnim3 = Switch3.transform.GetChild(1).gameObject.GetComponent<Animator>();
        SAnim4 = Switch4.transform.GetChild(1).gameObject.GetComponent<Animator>();
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
                doorAnimator.Play("GridUp");
                Invoke("startCutScene", 2.5f);
                isDone = true;
            }
        }
    }

    void startCutScene()
    {
        cutscene.SetActive(true);
        Invoke("showSuccess", 5.0f);
     
    }

    void showSuccess()
    {
        cutscene.SetActive(false);
        successM.SetActive(true);
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
                        SAnim1.SetBool("LeverUp",!SAnim1.GetBool("LeverUp"));
                        toggleTorch(torch1);
                        toggleTorch(torch3);
                        toggleTorch(torch4);

                    }
                    if (hit.transform.gameObject.name == Switch2.name)
                    {
                        SAnim2.SetBool("LeverUp", !SAnim2.GetBool("LeverUp"));
                        toggleTorch(torch2);
                        toggleTorch(torch4);

                    }
                    if (hit.transform.gameObject.name == Switch3.name)
                    {
                        SAnim3.SetBool("LeverUp", !SAnim3.GetBool("LeverUp"));
                        toggleTorch(torch2);
                        toggleTorch(torch3);
                        toggleTorch(torch4);

                    }
                    if (hit.transform.gameObject.name == Switch4.name)
                    {
                        SAnim4.SetBool("LeverUp", !SAnim4.GetBool("LeverUp"));
                        toggleTorch(torch3);
                        toggleTorch(torch4);

                    }

                }

            }
        }
       
    }
    private void toggleTorch(GameObject torch)
    {
        if (torch.activeSelf == true)
        {
            torch.SetActive(false);
            torchCount--;
        }
        else
        {
            torch.SetActive(true);
            torchCount++;
        }
    }
}
