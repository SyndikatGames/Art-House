using System;
using VG2.Internal;

namespace VG2
{
    public abstract class PurchaseService : Service
    {
        public abstract string GetPriceString(string key_product);
        public abstract void Purchase(string key_product, Action<bool> onSuccess);

    }
}



