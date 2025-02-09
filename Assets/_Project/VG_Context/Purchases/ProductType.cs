using System.Collections.Generic;

namespace VG2
{
    public enum ProductType
    {
        None = 0,
        NoAds = 1,
        LegendaryBox = 2,
        SmallBoxPack = 3,
        BigBoxPack = 4,
    }

    public static class Products
    {
        public static ProductType GetProductTypeByKey(string key)
        {
            foreach (var info in Infos)
                if (info.Value.key == key) return info.Key;

            return ProductType.None;
        }




        public static readonly Dictionary<ProductType, ProductInfo> Infos = new Dictionary<ProductType, ProductInfo>
        {
            // Non Consumables
            {
                ProductType.NoAds,
                new ProductInfo { type = ProductType.NoAds, key = "no_ads", isConsumable = false }
            },

            // Consumables
            {
                ProductType.LegendaryBox,
                new ProductInfo { type = ProductType.LegendaryBox, key = "legendary_box", isConsumable = true }
            },
            {
                ProductType.SmallBoxPack,
                new ProductInfo { type = ProductType.SmallBoxPack, key = "small_box_pack", isConsumable = true }
            },
            {
                ProductType.BigBoxPack,
                new ProductInfo { type = ProductType.BigBoxPack, key = "big_box_pack", isConsumable = true }
            },


        };
            
    }


}
