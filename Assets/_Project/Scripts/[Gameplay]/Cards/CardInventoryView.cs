using UnityEngine;
using UnityEngine.UI;
using VG2;
using R3;

public class CardInventoryView : ReactiveView
{
    private static CardInventoryView _instance;

    [SerializeField] private GridLayoutGroup _grid;

    
    
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
        var cardList = GameState.CurrentRoom.cards;

        foreach (Transform child in _grid.transform)
            Destroy(child.gameObject);

        foreach (var cardData in cardList)
            Instantiate(ConfigHub.Cards.InventoryCardPrefab, _grid.transform).SetData(cardData);

    }


}
