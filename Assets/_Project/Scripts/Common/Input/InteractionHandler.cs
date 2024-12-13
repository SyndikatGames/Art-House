using System;
using UnityEngine;
using UnityEngine.EventSystems;
using VG;

public class InteractionHandler : MonoBehaviour, IDragHandler, 
    IBeginDragHandler, IEndDragHandler, IPointerClickHandler, 
    IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    public event Action onBeginDrag;
    public event Action<PointerEventData> onDrag;
    public event Action<PointerEventData> onEndDrag;
    public event Action<PointerEventData> onClick;
    public event Action<PointerEventData> onPointerEnter;
    public event Action<PointerEventData> onPointerExit;
    public event Action<PointerEventData> onDrop;

    private bool IsMobileControl => DeviceInfo.ControlType == VG.ControlType.Mobile;
    private bool IsDesctopControl => DeviceInfo.ControlType == VG.ControlType.Desktop;

    private bool _dragging = false;
    private bool _beginDragSuccess;


    // === Mobile control ===

    private bool _holdingForBeginDrag = false;
    private float _holdingDuration;
    private const float beginDragHoldingThreshold = 0.35f;

    private bool _hasOneClick = false;
    private float _timeSinceOneClick = 0f;
    private const float doubleClickThreshold = 0.5f;

    // ======================


    public bool DisableDragAction() => _beginDragSuccess = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        print(eventData.pointerId);
        
        if (IsDesctopControl)
        {
            _beginDragSuccess = true;
            onBeginDrag?.Invoke();
        }
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_beginDragSuccess) 
            onDrag?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!_beginDragSuccess) return;

        if (_dragging && _beginDragSuccess)
        {
            _dragging = false;
            onEndDrag?.Invoke(eventData);
        }
            
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsDesctopControl)
        {
            if (_dragging) _dragging = false;
            else onClick?.Invoke(eventData);
        }

        if (IsMobileControl)
        {
            if (_hasOneClick)
            {
                if (_timeSinceOneClick < doubleClickThreshold)
                {
                    onClick?.Invoke(eventData);
                    _timeSinceOneClick = 0;
                    _hasOneClick = false;
                }
            }
            else _hasOneClick = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData) 
        => onPointerEnter?.Invoke(eventData);

    public void OnPointerExit(PointerEventData eventData)
        => onPointerExit?.Invoke(eventData);

    public void OnDrop(PointerEventData eventData) 
        => onDrop?.Invoke(eventData);

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsMobileControl)
        {
            _holdingDuration = 0f;
            _beginDragSuccess = false;
            _holdingForBeginDrag = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsMobileControl)
        {
            _holdingForBeginDrag = false;

            if (_beginDragSuccess && !_dragging)
                onEndDrag?.Invoke(eventData);
        }
            
           
    }

    private void Update()
    {
        if (!IsMobileControl) return;

        if (_holdingForBeginDrag)
        {
            _holdingDuration += Time.deltaTime;
            if (_holdingDuration > beginDragHoldingThreshold)
            {
                _holdingForBeginDrag = false;
                _beginDragSuccess = true;
                onBeginDrag?.Invoke();
            }
        }

        if (_hasOneClick)
        {
            _timeSinceOneClick += Time.deltaTime;
            if (_timeSinceOneClick > doubleClickThreshold)
            {
                _hasOneClick = false;
                _timeSinceOneClick = 0f;
            }
        }
            
    }

}
