using System;
using System.Collections;
using UnityEngine;
using VG2.Internal;


namespace VG2
{
    public class Purchases : Manager
    {
        public delegate void OnPurchased(string productKey, bool success);
        public static event OnPurchased onPurchased;

        private static Purchases instance;

        private static PurchaseService service => instance.supportedService as PurchaseService;
        protected override string managerName => "VG IAP";


        [SerializeField] private ProductCatalog _productCatalog;

        public override void Initialize()
        {
            StartCoroutine(InitializeWithSaves());
        }

        private IEnumerator InitializeWithSaves()
        {
            yield return new WaitUntil(() => Saves.Initialized);

            supportedService = GetSupportedService();
            supportedService.onInitialized += InitCompleted;
            supportedService.Initialize();
            
        }

        protected override void OnInitialized()
        {
            instance = this;
            //Saves.Commit();
            Log(Core.Message.Initialized(managerName));
        }

        public static string GetPriceString(string productKey)
        {
            if (instance._productCatalog.ProductExists(productKey) == false)
                Core.Error.ProductDoesNotExists(instance.managerName, productKey);

            return service.GetPriceString(productKey);
        }



        public static void Purchase(string productKey, Action<bool> onSuccess = null)
        {
            if (instance._productCatalog.ProductExists(productKey) == false)
                Core.Error.ProductDoesNotExists(instance.managerName, productKey);

            instance.Log("Product purchase processing: " + productKey);

            service.Purchase(productKey, (success) =>
            {
                if (success)
                {
                    instance.Log("On purchased: " + productKey);
                    PurchasesHandler.HandlePurchase(productKey);
                    Saves.Save();
                }
                else instance.Log("On not purchased: " + productKey);

                onSuccess?.Invoke(success);
                onPurchased?.Invoke(productKey, success);
            });
        }


    }

}
