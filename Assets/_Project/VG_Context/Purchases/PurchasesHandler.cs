namespace VG2
{
    public static class PurchasesHandler
    {
        public static bool ProductPurchased(string productKey)
        {
            /*
            switch (productKey)
            {
                case Key_Product.no_ads:
                    return Saves.Bool[Key_Save.ads_enabled].Value == false;
                    
                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            */
            return false;
        }



        public static void HandlePurchase(string productKey)
        {
            /*
            switch (productKey)
            {
                case Key_Product.no_ads:
                    Saves.Bool[Key_Save.ads_enabled].Value = false;
                    break;

                case Key_Product.epic_box:
                    Saves.AddBoxes(BoxType.Epic, 1);
                    break;

                case Key_Product.fantastic_box:
                    Saves.AddBoxes(BoxType.Fantastic, 1);
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            */
            
        }


    }
}

