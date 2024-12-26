using System;
using UnityEngine;
using VG2;

public class ShopController
{

    private BoxWindowView _boxWindow;

    public void OpenShop()
    {
        DiContainer.Create(ConfigHub.Shop.ShopPrefab, UI.Canvas);
    }

    public void OpenBoxWindow(BoxType boxType)
    {
        _boxWindow = DiContainer.Create(ConfigHub.Shop.BoxWindowPrefab, UI.Canvas);
        _boxWindow.Display(boxType);
    }

    public void OpenInAppPurchaseWindow(string productKey)
    {

    }



    public void RunBoxPurchasing(BoxType boxType, int amount, Action<bool> onSuccess)
    {
        float price = ConfigHub.BaseValues.GetBoxPrice(boxType) * amount;

        if (GameState.money.Value >= price)
        {
            GameState.money.Value -= price;
            Debug.Log("Purchased " + amount);
            BoxCalculator.AddBoxes(boxType, amount);

            onSuccess?.Invoke(true);
        }

        onSuccess?.Invoke(false);
    }





}
