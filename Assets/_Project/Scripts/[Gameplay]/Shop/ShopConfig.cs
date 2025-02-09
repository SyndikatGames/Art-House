using System.Collections.Generic;
using UnityEngine;
using VG2;

[CreateAssetMenu(menuName = "Project/Shop", fileName = "Shop")]
public class ShopConfig : ScriptableObject
{

    [System.Serializable]
    private struct InAppPurchaseWindow
    {
        public ProductType productKey;
        public GameObject prefab;
    }

    [System.Serializable]
    private struct BoxPackProduct
    {
        public ProductType productKey;
        public List<BoxPack> boxes;
    }


    [System.Serializable]
    private struct BoxPack
    {
        public BoxType boxType;
        public int amount;
    }



    [field: SerializeField] public ShopView ShopPrefab { get; private set; }
    [field: SerializeField] public BoxWindowView BoxWindowPrefab { get; private set; }
    [field: SerializeField] public SellCardsWindowView SellCardsWindowPrefab { get; private set; }


    [field: SerializeField] public int BoxesInGroup { get; private set; }

    [SerializeField] private List<BoxPackProduct> _boxPackProducts;

    [SerializeField] private List<InAppPurchaseWindow> _inAppPurchaseWindows;


    public GameObject GetInAppPurchaseWindowPrefab(ProductType productKey)
        => _inAppPurchaseWindows.Find(item => item.productKey == productKey).prefab;


    public Dictionary<BoxType, int> GetBoxesFromBoxPack(ProductType boxPackProductKey)
    {
        var boxPack = _boxPackProducts.Find(item => item.productKey == boxPackProductKey);

        var result = new Dictionary<BoxType, int>();
        foreach (var boxAmount in boxPack.boxes)
            result.Add(boxAmount.boxType, boxAmount.amount);

        return result;
    }


}