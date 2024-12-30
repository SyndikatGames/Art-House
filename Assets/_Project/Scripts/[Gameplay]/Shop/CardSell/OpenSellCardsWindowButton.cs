using VG2;
using Zenject;

public class OpenSellCardsWindowButton : ButtonHandler
{
    [Inject] private ShopController _shopController;

    protected override void OnClick()
    {
        _shopController.OpenSellCardsWindow();
    }
}
