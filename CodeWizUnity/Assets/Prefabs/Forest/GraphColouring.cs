using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphColouring : MonoBehaviour
{
    [SerializeField] 
    GameObject[] edges;
    /*[SerializeField]
    GameObject[] nodes;*/
    [SerializeField]
    GameObject[] gems;
    int[,] matrix = new int[6, 6];
    GameObject selectedObject;
    char selectedGemColor;
    bool canPlace;
    int correctCount = 0;
    //Vector3[] startPos = new Vector3[5];

    char[] colors = new char[6];

    private void Start()
    {
        /*for (int i = 0; i < startPos.Length; i++)
        {
            startPos[i] = gems[i].transform.position;
        }*/
        for (int i = 0; i < 6; i++)
        {
            string str = edges[i].name.ToString();
            setMatrix(str.ToCharArray());
        }        
        for(int i = 1; i < 6; i++)
        {
            for(int j = 1; j < 6; j++)
            {
/*                Debug.Log(matrix[i, j]);*/
            }
        }
    }
    Vector3 startPos=Vector3.zero;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
                
            /*Debug.Log("Clicked");*/
            if (selectedObject == null)
            {

                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }
                    
                    selectedObject = hit.collider.gameObject;
                    startPos =selectedObject.transform.position;
                    char[] tempGem = selectedObject.name.ToCharArray();
                    selectedGemColor = tempGem[0];
                    /*Debug.Log(selectedGemColor);*/
                    
                    Cursor.visible = false;
                }
            }
            else
            {
                canPlace = true;
                RaycastHit placehit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out placehit))
                {
                    if (placehit.rigidbody.CompareTag("node"))
                    {
                        char[] nodeStr = placehit.rigidbody.gameObject.name.ToString().ToCharArray();
                        int nodeColorIndex = (int)char.GetNumericValue(nodeStr[nodeStr.Length - 1]);
                        for(int j=1;j<6;j++)
                        {
                            if (matrix[nodeColorIndex, j] == 1)
                            {
                                if (colors[j] == selectedGemColor)
                                {
                                    canPlace = false;
                                    selectedObject.transform.position = startPos;
                                    startPos = Vector3.zero;
                                    Cursor.visible = true;
                                }
                            }
                        }
                        if (canPlace)
                        {
                            colors[nodeColorIndex] = selectedGemColor;
                            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
                            selectedObject.transform.position = new Vector3(worldPosition.x, 0.0f, worldPosition.z);
                            selectedObject = null;
                            Cursor.visible = true;
                            correctCount++;
                            if(correctCount == 5)
                            {
                                
                                    Debug.Log("Completed!");
                                
                            }
                        }
                    }
                }
                
            }
        }

        if (selectedObject != null&&startPos!=Vector3.zero)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 0.10f, worldPosition.z);

        }
    }

    void setMatrix(char[] objName)
    {
        char chi = objName[objName.Length - 2];
        int i = (int)char.GetNumericValue(chi);
        char chj = objName[objName.Length - 1];
        int j = (int)char.GetNumericValue(chj);
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

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject)
        {
            Debug.Log("Collision Detected");
        }
    }
}
