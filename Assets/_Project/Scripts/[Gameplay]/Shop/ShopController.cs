using UnityEngine;
using VG2;

public class ShopController
{
    private SellCardsWindowView _sellCardsWindow;
    private BoxWindowView _boxWindow;
    private GameObject _inAppPurchaseWindow;


    public void OpenSellCardsWindow()
    {
        _sellCardsWindow = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Shop.SellCardsWindowPrefab, UI.Canvas);
    }

    public void SellCards()
    {
        float totalMoney = 0f;
        foreach (var cardModel in GameState.CurrentRoom.cards)
            totalMoney += ConfigHub.BaseValues.GetItemSellPrice(cardModel.rarityType) * cardModel.amount;

        GameState.money.Value += totalMoney;
        GameState.CurrentRoom.cards.Clear();
        Object.Destroy(_sellCardsWindow.gameObject);
    }


    public void OpenShop()
    {
        SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Shop.ShopPrefab, UI.Canvas);
    }

    public void OpenBoxWindow(BoxType boxType)
    {
        _boxWindow = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Shop.BoxWindowPrefab, UI.Canvas);
        _boxWindow.Display(boxType);
    }

    public void OpenInAppPurchaseWindow(ProductKey productKey)
    {
        Purchases.onPurchased += OnProductPurchased;

        _inAppPurchaseWindow = SceneContainer.InstantiatePrefab
            (ConfigHub.Shop.GetInAppPurchaseWindowPrefab(productKey), UI.Canvas);

        
        if (productKey == ProductKey.FantasticBox)
            _inAppPurchaseWindow.GetComponent<InAppPurchaseBoxWindowView>()
                .Display(BoxType.Fantastic, ProductKey.FantasticBox);
        
        else _inAppPurchaseWindow.GetComponent<InAppPurchaseBoxPackWindowView>()
                .Display(productKey);


        _inAppPurchaseWindow.GetComponent<GameObjectEvents>().onDestroy += 
            () => Purchases.onPurchased -= OnProductPurchased;
    }

    private void OnProductPurchased(ProductKey productKey, bool success)
    {
        UnityEngine.Object.Destroy(_inAppPurchaseWindow);
    }



    public bool TryPurchaseBox(BoxType boxType, int amount)
    {
        float price = ConfigHub.BaseValues.GetBoxPrice(boxType) * amount;

        if (GameState.money.Value >= price)
        {
            GameState.money.Value -= price;
            BoxCalculator.AddBoxes(boxType, amount);

            return true;
        }

        return false;
    }





}
