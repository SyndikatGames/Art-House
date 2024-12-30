using System;
using VG2.Internal;

namespace VG2
{
    public abstract class PurchaseService : Service
    {
        public abstract string GetPriceString(ProductKey productKey);
        public abstract void Purchase(ProductKey productKey, Action<bool> onSuccess);

    }
}



