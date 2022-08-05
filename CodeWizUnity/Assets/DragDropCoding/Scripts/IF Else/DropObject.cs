using DragDropScripts.IfElse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropObject : MonoBehaviour, IDropHandler
{
    private IfElseHandler handler;
    [SerializeField]
    public GameObject top;
    public int childCount=0;
    public int blockPoint=4;
    private void Awake()
    {
        handler = GameObject.Find("Scripts").GetComponent<IfElseHandler>();
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(this.transform.parent.name);
        if (eventData.pointerDrag != null)
        {
            RectTransform draggedObject = eventData.pointerDrag.GetComponent<RectTransform>();
            
            DraggableObject draggableObject = draggedObject.GetComponent<DraggableObject>();
            draggableObject.parent = this.gameObject.transform.parent.GetChild(blockPoint).gameObject;
            if(draggedObject.transform.GetChild(1).name=="SnapObject")
                draggedObject.transform.GetChild(1).gameObject.SetActive(true);
            draggableObject.validDrop = true;
            draggableObject.childNumber = childCount;
            childCount++;

            RectTransform topRect = top.GetComponent<RectTransform>();
            
            draggedObject.position = topRect.position;
            topRect.position -= new Vector3(0, (draggedObject.sizeDelta.y + handler.Gap) * handler.canvas.scaleFactor, 0);
        }
            
    }
}
