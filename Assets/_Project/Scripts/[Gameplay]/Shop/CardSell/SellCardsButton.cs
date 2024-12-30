using VG2;
using Zenject;

public class SellCardsButton : ButtonHandler
{
    [Inject] private ShopController _shopController;

    protected override void OnClick()
    {
        _shopController.SellCards();
    }
}
