using UnityEngine;
using VG2;
using Zenject;

public class OpenInAppPurchaseWindowButton : ButtonHandler
{
    [SerializeField] private InAppPurchaseView _inAppPurchaseView;
    [Inject] private ShopController _shopController;

    protected override void OnClick()
    {
        _shopController.OpenInAppPurchaseWindow(_inAppPurchaseView.ProductKey);
    }


}
