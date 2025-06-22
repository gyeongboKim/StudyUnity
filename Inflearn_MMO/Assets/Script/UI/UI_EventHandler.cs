using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{

    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> onDragHandler = null;

    public void OnDrag(PointerEventData eventData)
    {
        if (onDragHandler != null)
            onDragHandler.Invoke(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }
}
