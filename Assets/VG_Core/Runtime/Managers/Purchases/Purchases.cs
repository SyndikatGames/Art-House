using System;
using System.Collections;
using UnityEngine;
using VG2.Internal;


namespace VG2
{
    public class Purchases : Manager
    {
        public delegate void OnPurchased(ProductKey productKey, bool success);
        public static event OnPurchased onPurchased;

        private static Purchases instance;

        private static PurchaseService service => instance.supportedService as PurchaseService;
        protected override string managerName => "VG IAP";

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

        public static string GetPriceString(ProductKey productKey) => service.GetPriceString(productKey);




        public static void Purchase(ProductKey productKey, Action<bool> onSuccess = null)
        {
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
