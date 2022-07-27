using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField]
    private int animatNo;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("Movement", animatNo);
        if (animatNo == 4)
            InvokeRepeating("SetRandomInteraction", 0f, 2f);
    }
    void SetRandomInteraction()
    {
        int animation = Random.Range(0,4);
        animator.SetInteger("Random Parameter", animation);
    }
   
}
