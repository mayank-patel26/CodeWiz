using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBaseCollider : MonoBehaviour
{

    [SerializeField]
    GameObject baseCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == baseCollider)
        {
            GameObject node = baseCollider.GetComponentInParent<GameObject>() as GameObject;
            Debug.Log(node.name.ToString());
        }
    }
}
