using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;

public class RoomExpansionWindowView : ReactiveView
{

    [SerializeField] private TextMeshProUGUI _totalMoneyText;
    [SerializeField] private Button _buyButton; 
    
    public RectTransform BuyButtonRect => _buyButton.transform as RectTransform;


    protected override void Subscribe() { }

    protected override void Display()
    {
        float price = ConfigHub.BaseValues.
            GetRoomExpansionPrice(GameState.CurrentRoom.expansionLevel.Value);

        _buyButton.interactable = GameState.money.Value >= price;
        _totalMoneyText.text = $"<sprite=0>{price.ToShortNumber()}";
    }



}
