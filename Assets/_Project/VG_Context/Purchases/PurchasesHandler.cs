namespace VG2
{
    public static class PurchasesHandler
    {
        public static void HandlePurchase(string productKey)
        {
            
            switch (productKey)
            {
                case Key_Product.no_ads:
                    GameState.adsEnabled.Value = false;
                    break;

                case Key_Product.fantastic_box:
                    BoxCalculator.AddBoxes(BoxType.Fantastic, 1);
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            
            
        }


    }
}

