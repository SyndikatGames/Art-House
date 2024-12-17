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
    private CardMoveHandler _cardMoveHandler;

    private Vector2 _pointerWorldPosition;
    private ItemInteractionHandler _interactionHandler;


    private Camera _camera;
    private Camera Camera { get { if (_camera == null) _camera = Camera.main; return _camera; } }

    private void Awake()
    {
        _moveHandler = new ItemMoveHandler(_item);
        _mergeHandler = new ItemMergeHandler(_item);
        _cardMoveHandler = new CardMoveHandler(_item);

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

        if (_cardMoveHandler.Release() == CardState.InsideCatalog)
        {
            Sound.Play(Key_Sound.PlaceItem);
            return;
        }

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

    public void Drag(Vector2 pointerScreenPosition)
    {
        if (_cardMoveHandler.Move(pointerScreenPosition) == CardState.World)
        {
            Vector2 worldPosition = Camera.ScreenToWorldPoint(pointerScreenPosition);

            _itemMerged = false;
            _pointerWorldPosition = worldPosition;
            _moveHandler.OnDrag(worldPosition);
        }

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
