/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    //no of rings, from shlok's formula
    private int n;
    
    
    [SerializeField]
    private GameObject ring1;
    [SerializeField]
    private GameObject ring2;
    [SerializeField]
    private GameObject ring3;
    [SerializeField]
    private GameObject ring4;


    //1 unit rotation = 45 degree clockwise
    //rotation speed, degrees per second
    float rotationSpeed = 30.0f;

    //no of rotations is timeLeft, n is number of seconds for which the object will rotate
    float timeLeft = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft > 0)
        {
            ring1.transform.Rotate(Vector3.fwd, rotationSpeed * Time.deltaTime);
        }
        
    }

}
*/