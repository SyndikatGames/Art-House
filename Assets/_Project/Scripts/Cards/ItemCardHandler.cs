using UnityEngine;
using VG2;

public enum CardState { InsideCatalog, World }


public class ItemCardHandler
{
    private Item _item;
    private ItemCard _card;

    private RectTransform _cardRect;
    private Camera _camera;
    private Vector2 _currentScreenPosition;


    private const float convertToItemThresholdViewportY = 0.12f;

    public ItemCardHandler(Item item)
    {
        _item = item;
    }

    private void Initialize()
    {
        _camera = Camera.main;
        _card = Object.Instantiate(Prefabs.ItemCard, UI.Canvas);
        _cardRect = _card.GetComponent<RectTransform>();

        _card.SetData(new CardModel
        {
            itemType = _item.ItemType,
            rarityType = _item.RarityType,
            amount = 1,
        });
        _cardRect.gameObject.SetActive(false);
        _card.DisableRaycast();
    }

    public CardState Move(Vector2 screenPosition)
    {
        if (_card == null) Initialize();

        _currentScreenPosition = screenPosition;

        bool insideCatalog = _camera.ScreenToViewportPoint(screenPosition).y < convertToItemThresholdViewportY;
        _card.gameObject.SetActive(insideCatalog);
        _cardRect.position = screenPosition;


        if (insideCatalog)
        {
            _item.Effects.SetInvisible();
            CardInventoryView.Grid.enabled = false;
        }

        return insideCatalog ? CardState.InsideCatalog : CardState.World;
    }

    public CardState Release()
    {
        bool insideCatalog = _camera.ScreenToViewportPoint(_currentScreenPosition).y 
            < convertToItemThresholdViewportY;

        if (insideCatalog) _item.Destroy();

        _card.transform.SetParent(CardInventoryView.Grid.transform);

        CardInventoryView.Grid.enabled = true;

        var cardData = new CardModel
        {
            rarityType = _item.RarityType,
            itemType = _item.ItemType,
            amount = 1,
        };

        if (!insideCatalog && CardMoving.MovingItemFromCard)
            CardCalculator.RemoveCard(cardData);

        else if (insideCatalog && !CardMoving.MovingItemFromCard)
            CardCalculator.AddCard(cardData);

        //else Saves.UpdateCards();

        return insideCatalog ? CardState.InsideCatalog : CardState.World;
    }


}
