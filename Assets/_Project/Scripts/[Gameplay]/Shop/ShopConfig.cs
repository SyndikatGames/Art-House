using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Project/Shop", fileName = "Shop")]
public class ShopConfig : ScriptableObject
{

    [System.Serializable]
    private struct InAppPurchaseWindow
    {
        public string productKey;
        public GameObject prefab;
    }



    [field: SerializeField] public ShopView ShopPrefab { get; private set; }
    [field: SerializeField] public BoxWindowView BoxWindowPrefab { get; private set; }

    [field: SerializeField] public int BoxesInGroup { get; private set; }

    [SerializeField] private List<InAppPurchaseWindow> _inAppPurchaseWindows;


    public GameObject GetInAppPurchaseWindowPrefab(string productKey)
        => _inAppPurchaseWindows.Find(item => item.productKey == productKey).prefab;




}