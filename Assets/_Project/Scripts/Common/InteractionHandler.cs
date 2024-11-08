using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionHandler : MonoBehaviour, IDragHandler, 
    IBeginDragHandler, IEndDragHandler, IPointerClickHandler, 
    IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public event Action<PointerEventData> onBeginDrag;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onEndDrag;
    public event Action<PointerEventData> onClick;
    public event Action<PointerEventData> onPointerEnter;
    public event Action<PointerEventData> onPointerExit;
    public event Action<PointerEventData> onDrop;

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

    public void OnPointerEnter(PointerEventData eventData) 
        => onPointerEnter?.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData)
        => onPointerExit?.Invoke(eventData);

    public void OnDrop(PointerEventData eventData) 
        => onDrop?.Invoke(eventData);
}
