using UnityEngine;
using UnityEngine.UI;
using VG;

public class CardCatalog : Info
{
    public static GridLayoutGroup Grid { get; private set; }

    [SerializeField] private ItemCard _cardPrefab;
    [SerializeField] private GridLayoutGroup _grid;


    private void Awake()
    {
        Grid = _grid;
    }


    protected override void Subscribe()
    {
        Saves.String[Key_Save.cards_data(0)].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.String[Key_Save.cards_data(0)].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
    {
        _grid.enabled = true;
        var cardList = Saves.GetCards();

        foreach (Transform child in _grid.transform)
            Destroy(child.gameObject);

        foreach (var cardData in cardList)
            Instantiate(_cardPrefab, _grid.transform).SetData(cardData);

    }


}
