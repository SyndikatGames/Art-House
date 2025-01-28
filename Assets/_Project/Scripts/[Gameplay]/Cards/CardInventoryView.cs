using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VG2;

public class CardInventoryView : ReactiveView
{
    private static CardInventoryView _instance;

    [SerializeField] private GameObject _scroller;
    [SerializeField] private GridLayoutGroup _grid;
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private RectTransform _viewportRect;
    [SerializeField] private ContentSizeFitter _sizeFitter;

    private List<InventoryCard> _cards = new();

    
    public static Transform CardContainer => _instance._grid.transform;
    public static void UpdateView() => _instance.Display();
    public static void SetGridActive(bool active) => _instance._grid.enabled = active;


    private void Awake()
    {
        _instance = this;
    }

    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.cards.onChanged.Subscribe(_ => Display()));
    }

    protected override void Display()
    {
        _grid.enabled = true;
        _cards.Clear();
        var cardList = GameState.CurrentRoom.cards;

        foreach (Transform child in _grid.transform)
            Destroy(child.gameObject);

        foreach (var cardData in cardList)
        {
            var card = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Cards.InventoryCardPrefab, _grid.transform);
            card.SetData(cardData);
            _cards.Add(card);
        }

    }


    public InventoryCard GetAnyCardByList(List<ItemType> itemTypes)
    {
        foreach (var itemType in itemTypes)
        {
            var card = _cards.Find(card => card.Data.itemType == itemType);
            if (card != null) return card;
        }

        return null;
    }
        


}
