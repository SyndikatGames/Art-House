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

    public override void DisableDragging() => _beginDragSuccess = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_beginDragSuccess)
            itemInteraction.Drag(eventData.position);
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
        if (MobileControl.CurrentState == MobileControl.State.Free)
            itemInteraction.Click();
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

        if (MobileControl.CurrentState == MobileControl.State.ItemMoving)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                MobileControl.CurrentState = MobileControl.State.Free;
        }
            
    }

    
}
