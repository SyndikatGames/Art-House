using UnityEngine;
using UnityEngine.EventSystems;

public class MobileItemInteraction : ItemInteractionHandler, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler,
    IPointerEnterHandler, IPointerExitHandler, IDropHandler, IPointerDownHandler, IPointerUpHandler
{
    private Touch _currentTouch;
    private bool _dragging = false;
    private bool _beginDragSuccess;

    private bool _holdingForBeginDrag = false;
    private float _holdingDuration;
    private const float beginDragHoldingThreshold = 0.25f;

    private bool _hasOneClick = false;
    private float _timeSinceOneClick = 0f;
    private const float doubleClickThreshold = 0.5f;

    public override void DisableDragging() => _beginDragSuccess = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerWorldPosition = Camera.ScreenToWorldPoint(eventData.position);

        if (_beginDragSuccess)
            itemInteraction.Drag(pointerWorldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragging && _beginDragSuccess)
        {
            _dragging = false;
            itemInteraction.EndDrag();
        }   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_hasOneClick)
        {
            if (_timeSinceOneClick < doubleClickThreshold)
            {
                itemInteraction.Click();
                _timeSinceOneClick = 0;
                _hasOneClick = false;
            }
        }
        else _hasOneClick = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
        => itemInteraction.PointerEnter();

    public void OnPointerExit(PointerEventData eventData)
    {
        _holdingForBeginDrag = false;
        itemInteraction.PointerExit();
    }

    public void OnDrop(PointerEventData eventData) 
        => itemInteraction.Drop();

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ItemMoveHandler.DraggableItem == null)
        {
            _holdingDuration = 0f;
            _beginDragSuccess = false;
            _holdingForBeginDrag = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _holdingForBeginDrag = false;

        if (_beginDragSuccess && !_dragging)
            itemInteraction.EndDrag();
    }

    private void Update()
    {
        if (_holdingForBeginDrag)
        {
            _holdingDuration += Time.deltaTime;
            if (_holdingDuration > beginDragHoldingThreshold 
                && MobileControl.CurrentState == MobileControl.State.Free)
            {
                _holdingForBeginDrag = false;
                _beginDragSuccess = true;
                itemInteraction.BeginDrag();
                MobileControl.CurrentState = MobileControl.State.ItemMoving;
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

        if (MobileControl.CurrentState == MobileControl.State.ItemMoving)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                MobileControl.CurrentState = MobileControl.State.Free;
        }
            
    }

    
}
