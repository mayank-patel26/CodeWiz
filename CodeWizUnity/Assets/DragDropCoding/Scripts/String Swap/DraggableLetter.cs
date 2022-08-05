using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableLetter : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    GameObject clone;
    private Canvas canvas;
    private RectTransform rectTransform;
    private bool validDrop;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();        
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("BlockCanvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        clone = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, this.transform.parent);
        clone.name = this.name;
        startPosition = transform.position;
        canvasGroup.alpha = 0.8f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<RectTransform>().GetComponent<DraggableLetter>().validDrop = true;
        eventData.pointerDrag.transform.localPosition=this.transform.localPosition;
        Destroy(this.gameObject);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (validDrop)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }
        else
            this.transform.position = startPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
