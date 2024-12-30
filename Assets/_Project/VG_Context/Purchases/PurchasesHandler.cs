namespace VG2
{
    public static class PurchasesHandler
    {
        public static void HandlePurchase(ProductKey productKey)
        {
            
            switch (productKey)
            {
                case ProductKey.NoAds:
                    GameState.adsEnabled.Value = false;
                    break;

                case ProductKey.FantasticBox:
                    BoxCalculator.AddBoxes(BoxType.Fantastic, 1);
                    break;

                case ProductKey.SmallBoxPack: 
                case ProductKey.BigBoxPack:
                    foreach (var boxAmount in ConfigHub.Shop.GetBoxesFromBoxPack(productKey))
                        BoxCalculator.AddBoxes(boxAmount.Key, boxAmount.Value);
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            
            
        }


    }
}

