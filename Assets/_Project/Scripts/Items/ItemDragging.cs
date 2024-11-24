using UnityEngine;
using UnityEngine.EventSystems;
using VG;


public class ItemDragging : MonoBehaviour
{
    private static bool _itemMerged;


    [SerializeField] private Item _item;
    [SerializeField] private InteractionHandler _interactionHandler;
    [SerializeField] private GameObject _flashPrefab;

    private ItemMoveHandler _moveHandler;
    private ItemMergeHandler _mergeHandler;

    private void Awake()
    {
        _moveHandler = new ItemMoveHandler(_item);
        _mergeHandler = new ItemMergeHandler(_item, _flashPrefab);

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
        if (!_itemMerged)
        {
            _moveHandler.OnEndDrag(data);
            Sound.Play(Key_Sound.PlaceItem);
        }
    }

    private void OnDrop(PointerEventData data)
    {
        _itemMerged = _mergeHandler.OnDrop();
        if (_itemMerged) Sound.Play(Key_Sound.MergeItems);
    }

    private void OnDrag(PointerEventData data)
    {
        _itemMerged = false;
        _moveHandler.OnDrag(data);
    }

    private void OnBeginDrag(PointerEventData data)
    {
        if (_moveHandler.OnBeginDrag())
        {
            Sound.Play(Key_Sound.TakeItem);
            _mergeHandler.OnBeginDrag();
        }

        else
        {
            Sound.Play(Key_Sound.CanNotMoveItem);
            _interactionHandler.DisableDragAction();
        }
        
    }
}
