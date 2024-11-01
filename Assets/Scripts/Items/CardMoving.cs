using UnityEngine;
using UnityEngine.EventSystems;

public class CardMoving : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Item _itemPrefab;

    private Item _itemInstance;
    private CanvasGroup _canvasGroup;
    private RectTransform _rectTransform;
    private Transform _originParent;

    private const float convertY = 300f;

    private bool convertedToItem => _rectTransform.position.y > convertY;


    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originParent = _rectTransform.parent;
        _rectTransform.SetParent(UI.Canvas);

        if (_itemInstance != null)
            ItemList.Items.Remove(_itemInstance);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.position = eventData.position;

        if (convertedToItem)
        {
            if (_itemInstance == null)
                _itemInstance = Instantiate(_itemPrefab, null);

            Vector2 worldPointerPosition 
                = Camera.main.ScreenToWorldPoint(eventData.position);

            if (TryPlace(_itemInstance, worldPointerPosition, 
                out var resultPosition, out var resultGridPosition))
            {
                _itemInstance.transform.position = resultPosition;
                _itemInstance.SetTransparent(false);
                _itemInstance.Placing(resultGridPosition);
            }
            else
            {
                _itemInstance.transform.position = worldPointerPosition;
                _itemInstance.SetTransparent(true);
            }

            _canvasGroup.alpha = 0f;
        }
        else
        {
            if (_itemInstance != null) Destroy(_itemInstance.gameObject);
            _canvasGroup.alpha = 1f;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 worldPointerPosition
                = Camera.main.ScreenToWorldPoint(eventData.position);

        if (TryPlace(_itemInstance, worldPointerPosition, 
            out _, out var resultGridPosition))
        {
            _itemInstance.SetPlace(resultGridPosition);
            print(resultGridPosition);
        }
            
    }


    private bool TryPlace(Item item, Vector2 worldPosition, 
        out Vector2 resultWorldPosition, out Vector3Int resultGridPosition)
    {
        foreach (var placedItem in ItemList.Items)
        {
            foreach (var placeGrid in placedItem.PlaceGrids)
            {
                if (placeGrid.TryPlaceItem(item, worldPosition,
                    out resultWorldPosition, out resultGridPosition))
                    return true;
            }
        }

        resultWorldPosition = default;
        resultGridPosition = default;
        return false;
    }




}
