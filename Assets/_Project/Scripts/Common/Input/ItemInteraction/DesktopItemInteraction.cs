using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopItemInteraction : ItemInteractionHandler, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler,
    IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    private bool _dragging = false;

    public override void DisableDragging() => _dragging = false;


    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
        itemInteraction.BeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerWorldPosition = Camera.ScreenToWorldPoint(eventData.position);

        if (_dragging) 
            itemInteraction.Drag(pointerWorldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragging)
        {
            _dragging = false;
            itemInteraction.EndDrag();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_dragging) itemInteraction.Click();
    }

    public void OnPointerEnter(PointerEventData eventData)
        => itemInteraction.PointerEnter();

    public void OnPointerExit(PointerEventData eventData)
        => itemInteraction.PointerExit();

    public void OnDrop(PointerEventData eventData)
        => itemInteraction.Drop();


}
