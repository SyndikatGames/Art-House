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
    private bool _beginDragSuccess;

    public bool DisableDragAction() => _beginDragSuccess = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        _beginDragSuccess = true;
        onBeginDrag?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_beginDragSuccess) 
            onDrag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_beginDragSuccess)
            onEndDrag?.Invoke(eventData);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (_dragging) _dragging = false;
        else onClick?.Invoke(eventData);
    }
}
