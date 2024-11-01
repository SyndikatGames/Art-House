using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour, IDragHandler, 
    IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    public event Action<PointerEventData> onBeginDrag;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onEndDrag;
    public event Action<PointerEventData> onClick;

    private bool _dragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        onBeginDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
        => onDrag?.Invoke(eventData);

    public void OnEndDrag(PointerEventData eventData)
        => onEndDrag?.Invoke(eventData);


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_dragging) _dragging = false;
        else onClick?.Invoke(eventData);
    }
}
