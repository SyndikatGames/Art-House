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
            ItemList.Remove(_itemInstance);
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

            //GlobalGrid.TryPlacing(_itemInstance, worldPointerPosition);

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
        /*
        bool placeSuccess = GlobalGrid.TryPlacing(_itemInstance, worldPointerPosition);
        if (placeSuccess) _itemInstance.Place()*/
        
    }
}
