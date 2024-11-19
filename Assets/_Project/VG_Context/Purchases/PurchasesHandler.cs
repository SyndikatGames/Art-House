

namespace VG
{
    public static class PurchasesHandler
    {
        public static bool ProductPurchased(string productKey)
        {
            switch (productKey)
            {
                case Key_Product.no_ads:
                    return Saves.Bool[Key_Save.ads_enabled].Value == false;
                    
                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
        }



        public static void HandlePurchase(string productKey)
        {

            switch (productKey)
            {
                case Key_Product.no_ads:
                    Saves.Bool[Key_Save.ads_enabled].Value = false;
                    break;

                case Key_Product.epic_pack:
                    Saves.AddBoxes(RarityType.Epic, 5);
                    break;

                case Key_Product.fantastic_box:
                    Saves.AddBoxes(RarityType.Fantastic, 1);
                    break;

                case Key_Product.legendary_box:
                    Saves.AddBoxes(RarityType.Legendary, 1);
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            
        }


    }
}

