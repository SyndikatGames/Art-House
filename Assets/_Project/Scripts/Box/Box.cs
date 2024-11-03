using UnityEngine;
using DG.Tweening;

public class Box : MonoBehaviour
{

    [SerializeField] private BoxConfig _boxConfig;

    private Item _itemInstance;
    private Vector2 _originPosition;

    private const float itemMoveY = 0.5f;
    private const float showDuration = 0.5f;




    private void Start()
    {
        _originPosition = transform.position;
    }


    public void Open()
    {
        var itemPrefab = _boxConfig.GetRandomItem();
        _itemInstance = Instantiate(itemPrefab, _originPosition, Quaternion.identity);

        _itemInstance.transform.DOMoveY(_originPosition.y + itemMoveY, showDuration);

        _itemInstance.transform.localScale = Vector3.zero;
        _itemInstance.transform.DOScale(1f, showDuration);

        transform.DOMoveY(_originPosition.y - itemMoveY, showDuration);
        transform.DOScale(0f, showDuration);

        _itemInstance.onPlaced += OnItemPlaced;
    }

    private void OnItemPlaced()
    {
        _itemInstance.onPlaced -= OnItemPlaced;
        Show();

    }

    public void Show()
    {
        transform.DOMoveY(_originPosition.y, showDuration);
        transform.DOScale(1f, showDuration);
    }


}
