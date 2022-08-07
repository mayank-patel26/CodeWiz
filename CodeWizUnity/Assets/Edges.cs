using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edges : MonoBehaviour
{
    [SerializeField]
    GameObject[] edges;
    int[,] matrix = new int[5,5];

    private GameObject selectedObject;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < edges.Length; i++)
        {
            edges[i].SetActive(false);
        }

        for(int i = 0; i < edges.Length; i++)
        {
            int t = Random.Range(0, 2);
            if(t == 1)
            {
                edges[i].SetActive(true);
                string str = edges[i].name.ToString();
                Debug.Log(str);
                setMatrix(str);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(selectedObject == null)
            {
                RaycastHit hit = CastRay();
                if(hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            else
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
                selectedObject = null;
                Cursor.visible = true;
            }
        }

        if(selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.25f, worldPosition.z);
            
        }
    }

    void setMatrix(string objName)
    {
        int i = objName.Length - 2;
        int j = objName.Length - 1;
        matrix[i, j] = 1;
        matrix[j, i] = 1;
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);

        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        return hit;
    }
}
