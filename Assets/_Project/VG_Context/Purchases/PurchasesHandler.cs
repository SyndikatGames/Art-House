namespace VG2
{
    public static class PurchasesHandler
    {
        public static void HandlePurchase(ProductType productKey)
        {
            
            switch (productKey)
            {
                case ProductType.NoAds:
                    GameState.adsEnabled.Value = false;
                    break;

                case ProductType.LegendaryBox:
                    BoxCalculator.AddBoxes(BoxType.Fantastic, 1);
                    break;

                case ProductType.SmallBoxPack: 
                case ProductType.BigBoxPack:
                    foreach (var boxAmount in ConfigHub.Shop.GetBoxesFromBoxPack(productKey))
                        BoxCalculator.AddBoxes(boxAmount.Key, boxAmount.Value);
                    break;

                default: throw new System.Exception("Wrong product: " + productKey.ToString());
            }
            
            
        }


    }
}

