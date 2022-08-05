using DragDropScripts.IfElse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    private IfElseHandler handler;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    public GameObject parent;
    public int childNumber;
    private RectTransform rectTransform;
    
    public  bool validDrop = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        handler = GameObject.Find("Scripts").GetComponent<IfElseHandler>();
        canvas =handler.canvas;
        canvasGroup=GetComponent<CanvasGroup>();
        
    }

    private Vector3 startPosition;
    private GameObject clone;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!validDrop)
        {
            clone = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, handler.snippets.transform);
            clone.name = this.name;
        }
        else
        {
            validDrop = false;
            Vector3 currentPos = transform.position;
            int i=childNumber+1;
            for (; i < parent.transform.childCount; i++)
            {
                Transform current = parent.transform.GetChild(i);
                current.position = currentPos;
                current.gameObject.GetComponent<DraggableObject>().childNumber--;
                currentPos -= new Vector3(0, (current.GetComponent<RectTransform>().sizeDelta.y+handler.Gap) * handler.canvas.scaleFactor, 0);
            }
            parent.transform.parent.transform.GetChild(1).GetComponent<DropObject>().childCount = parent.transform.GetChild(i-1).GetComponent<DraggableObject>().childNumber;
            parent.transform.parent.transform.GetChild(0).transform.position = currentPos;
        }
        startPosition = transform.position;
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
        //Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (validDrop)
        {
            GameObject child = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, parent.transform);
            DraggableObject properties = child.GetComponent<DraggableObject>();
            properties.canvasGroup.alpha = 1f;
            properties.canvasGroup.blocksRaycasts=true;
        }
        transform.position = startPosition;
        Destroy(this.gameObject);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
       
    }
}
