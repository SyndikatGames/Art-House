using UnityEngine;
using VG;


public class PurchaseWithGems_Button : ButtonHandler
{
    [SerializeField] private GemProduct _product;

    protected override void OnClick()
    {
        int price = Configs.Shop.GetGemProductPrice(_product.ProductType);

        if (Saves.Float[Key_Save.soft_money].Value >= price)
        {
            Saves.Float[Key_Save.soft_money].Value -= price;
            Configs.Shop.HandleGemPurchase(_product.ProductType);
        }

    }


    
}