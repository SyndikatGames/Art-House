using System.Collections.Generic;

namespace VG2
{
    public enum ProductKey
    {
        None = 0,
        NoAds = 1,
        FantasticBox = 2,
        SmallBoxPack = 3,
        BigBoxPack = 4,
    }

    public static class Products
    {
        public static readonly List<ProductInfo> Infos = new List<ProductInfo>
        {
            new ProductInfo { productKey = ProductKey.NoAds, isConsumable = false },
            new ProductInfo { productKey = ProductKey.FantasticBox, isConsumable = true },
        };
    }


}
