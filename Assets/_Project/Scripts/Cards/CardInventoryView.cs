using UnityEngine;
using UnityEngine.UI;
using VG2;
using R3;

public class CardInventoryView : ReactiveView
{
    public static GridLayoutGroup Grid { get; private set; }

    [SerializeField] private ItemCard _cardPrefab;
    [SerializeField] private GridLayoutGroup _grid;


    private void Awake() => Grid = _grid;

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
            Instantiate(_cardPrefab, _grid.transform).SetData(cardData);

    }


}
