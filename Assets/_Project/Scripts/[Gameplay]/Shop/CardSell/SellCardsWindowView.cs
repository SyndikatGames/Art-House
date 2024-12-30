using TMPro;
using UnityEngine;
using VG2;

public class SellCardsWindowView : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _totalMoneyText;

    protected override void Subscribe() { }

    protected override void Display()
    {
        float totalMoney = 0f;
        foreach (var cardModel in GameState.CurrentRoom.cards)
            totalMoney += ConfigHub.BaseValues.GetItemSellPrice(cardModel.rarityType) * cardModel.amount;

        _totalMoneyText.text = $"<sprite=0>{totalMoney.ToShortNumber()}";
    }

    
}
