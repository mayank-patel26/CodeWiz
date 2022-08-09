using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DraggableLetter : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    GameObject clone;
    private Canvas canvas;
    private RectTransform rectTransform;
    private bool validDrop;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;
    StringReversal obj;
    private void Awake()
    {
        obj = GameObject.Find("Scripts").GetComponent<StringReversal>();
        rectTransform = GetComponent<RectTransform>();        
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GameObject.Find("BlockCanvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        clone = Instantiate(this.gameObject, this.gameObject.transform.position, this.gameObject.transform.rotation, this.transform.parent);
        clone.name = this.name;
        clone.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
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
        this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = eventData.pointerDrag.gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text;
        Destroy(eventData.pointerDrag.gameObject);
        obj.check();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!validDrop)
            this.transform.position = startPosition;

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
