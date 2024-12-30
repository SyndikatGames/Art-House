using System;
using System.Collections;
using System.Collections.Generic;
using VG2.YandexGames;
using UnityEngine;


namespace VG2
{
    public class YandexGames_PurchaseService : PurchaseService
    {
        private List<string> _purchasedProductIds;
        private Dictionary<string, string> _productPrices;



        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override string GetPriceString(ProductKey productKey)
        {
            if (!_productPrices.ContainsKey(productKey.ToString()))
                return string.Empty;

           return _productPrices[productKey.ToString()];
        }




        public override void Initialize()
        {
            Saves.onDeleted += OnSavesDeleted;
            StartCoroutine(PurchasesInitializing());
        }

        

        private IEnumerator PurchasesInitializing()
        {
            yield return new WaitUntil(() => YG_Purchases.available);

#if UNITY_WEBGL
                YG_Purchases.InitializePayments();
#endif

            YG_Purchases.GetPurchasedProducts((purchasedProductIds) =>
            {
                _purchasedProductIds = purchasedProductIds;
                foreach (var purchasedProductKey in _purchasedProductIds)
                {
                    if (purchasedProductKey == string.Empty) continue;

                    ProductKey productKey = (ProductKey)Enum.Parse(typeof(ProductKey), purchasedProductKey);
                    PurchasesHandler.HandlePurchase(productKey);
                    if (Products.Infos.Find(item => item.productKey == productKey).isConsumable)
                        YG_Purchases.Consume(purchasedProductKey);
                }
            });

            YG_Purchases.GetPrices((productPrices) 
                => _productPrices = productPrices);

            yield return new WaitUntil(
                () => _purchasedProductIds != null && _productPrices != null);

            InitCompleted();
        }

        public override void Purchase(ProductKey productKey, Action<bool> onSuccess)
        {
            onSuccess += (success) =>
            {
                if (success) YG_Purchases.Consume(productKey.ToString());
            };
            YG_Purchases.Purchase(productKey.ToString(), onSuccess);
        }


        private void OnSavesDeleted()
        {
            YG_Purchases.GetPurchasedProducts((purchasedProductIds) =>
            {
                foreach (var purchasedProductKey in purchasedProductIds)
                    YG_Purchases.Consume(purchasedProductKey);
            });
        }


    }
}


