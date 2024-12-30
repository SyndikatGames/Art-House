using UnityEngine;
using UnityEngine.EventSystems;
using VG2;

public class CardMoving : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static bool MovingItemFromCard { get; private set; } = false;


    [SerializeField] private InventoryCard _itemCard;
    [SerializeField] private GameObject _cardPanel;

    private ItemInteraction _itemInteraction;
    private const float convertToItemThresholdViewportY = 0.12f;


    public void OnBeginDrag(PointerEventData eventData)
    {
        var itemInstance = SceneContainer.InstantiatePrefabFromComponent(Prefabs.GetItem(_itemCard.Data.itemType));
        itemInstance.SetRarity(_itemCard.Data.rarityType);
        _itemInteraction = itemInstance.GetComponent<ItemInteraction>();
        _itemInteraction.BeginDrag();

        if (_itemCard.Data.amount == 1)
            _cardPanel.gameObject.SetActive(false);

        else
        {
            var cardData = _itemCard.Data;
            cardData.amount -= 1;
            _itemCard.SetData(cardData);
        }

        MovingItemFromCard = true;
    }

    public void OnDrag(PointerEventData eventData) => _itemInteraction.Drag(eventData.position);

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemInteraction.EndDrag();
        MovingItemFromCard = false;
    }


}
