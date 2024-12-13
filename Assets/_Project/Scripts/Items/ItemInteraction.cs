using System;
using UnityEngine;
using VG;


public class ItemInteraction : MonoBehaviour
{
    private static bool _itemMerged;

    public event Action onBeginDragging;


    [SerializeField] private Item _item;
    [SerializeField] private GameObject _interactableObject;

    private ItemMoveHandler _moveHandler;
    private ItemMergeHandler _mergeHandler;

    private Vector2 _pointerWorldPosition;
    private ItemInteractionHandler _interactionHandler;

    private void Awake()
    {
        _moveHandler = new ItemMoveHandler(_item);
        _mergeHandler = new ItemMergeHandler(_item);

        _interactionHandler = ItemInteractionHandler.GetHandler(_interactableObject);
        _interactionHandler.Initialize(this);
    }

    public void PointerExit()
    {
        _mergeHandler.OnPointerExit();
    }

    public void PointerEnter()
    {
        _mergeHandler.OnPointerEnter();
    }

    public void Click()
    {
        if (_moveHandler.OnClick())
            Sound.Play(Key_Sound.TakeItem);

        else Sound.Play(Key_Sound.CanNotMoveItem);
    }

    public void EndDrag()
    {
        _mergeHandler.OnEndDrag();
        if (!_itemMerged)
        {
            _moveHandler.OnEndDrag(_pointerWorldPosition);
            Sound.Play(Key_Sound.PlaceItem);
            Events.ItemPlaced();
        }
    }

    public void Drop()
    {
        _itemMerged = _mergeHandler.OnDrop();
        if (_itemMerged) Sound.Play(Key_Sound.MergeItems);
    }

    public void Drag(Vector2 pointerWorldPosition)
    {
        _itemMerged = false;
        _pointerWorldPosition = pointerWorldPosition;
        _moveHandler.OnDrag(pointerWorldPosition);
    }

    public void BeginDrag()
    {
        if (_moveHandler.OnBeginDrag())
        {
            Sound.Play(Key_Sound.TakeItem);
            _mergeHandler.OnBeginDrag();
            onBeginDragging?.Invoke();
        }

        else
        {
            Sound.Play(Key_Sound.CanNotMoveItem);
            _interactionHandler.DisableDragging();
        }
        
    }
}
