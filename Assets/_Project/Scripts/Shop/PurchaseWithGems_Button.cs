using UnityEngine;
using VG;


public class PurchaseWithGems_Button : ButtonHandler
{
    [SerializeField] private GemProduct _product;

    protected override void OnClick()
    {
        int price = Configs.Shop.GetGemProductPrice(_product.ProductType);

        if (Saves.Int[Key_Save.gems].Value >= price)
        {
            Saves.Int[Key_Save.gems].Value -= price;
            Configs.Shop.HandleGemPurchase(_product.ProductType);
        }

    }


    
}