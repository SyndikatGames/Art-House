using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDragging : MonoBehaviour
{
    private static bool _itemMerged;


    [SerializeField] private Item _item;
    [SerializeField] private InteractionHandler _interactionHandler;

    private ItemMoveHandler _moveHandler;
    private ItemMergeHandler _mergeHandler;

    private void Awake()
    {
        _moveHandler = new ItemMoveHandler(_item);
        _mergeHandler = new ItemMergeHandler(_item);

        _interactionHandler.onBeginDrag += OnBeginDrag;
        _interactionHandler.onDrag += OnDrag;
        _interactionHandler.onDrop += OnDrop;
        _interactionHandler.onEndDrag += OnEndDrag;
        _interactionHandler.onClick += OnClick;
        _interactionHandler.onPointerEnter += OnPointerEnter;
        _interactionHandler.onPointerExit += OnPointerExit;
    }

    private void OnPointerExit(PointerEventData data)
    {
        _mergeHandler.OnPointerExit();
    }

    private void OnPointerEnter(PointerEventData data)
    {
        _mergeHandler.OnPointerEnter();
    }

    private void OnClick(PointerEventData data)
    {
        _moveHandler.OnClick();
    }

    private void OnEndDrag(PointerEventData data)
    {
        _mergeHandler.OnEndDrag();
        if (!_itemMerged) _moveHandler.OnEndDrag(data);
    }

    private void OnDrop(PointerEventData data)
    {
        _itemMerged = _mergeHandler.OnDrop();
    }

    private void OnDrag(PointerEventData data)
    {
        _itemMerged = false;
        _moveHandler.OnDrag(data);
    }

    private void OnBeginDrag(PointerEventData data)
    {
        if (_moveHandler.OnBeginDrag())
            _mergeHandler.OnBeginDrag();

        else _interactionHandler.DisableDragAction();
        
    }
}
