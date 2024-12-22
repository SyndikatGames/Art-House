using VG;
using Zenject;

public class OpenShopButton : ButtonHandler
{
    [Inject] private ShopController _shopController;

    protected override void OnClick() => _shopController.OpenShop();
}
