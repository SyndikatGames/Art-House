using UnityEngine;
using VG;

[CreateAssetMenu(menuName = "Project/Shop", fileName = "Shop")]
public class ShopConfig : ScriptableObject
{
    [field: SerializeField] private int _rareBoxGemPrice;
    [field: SerializeField] private int _epicBoxGemPrice;


    public int GetGemProductPrice(GemProductType productType)
    {
        switch (productType)
        {
            case GemProductType.RareBox: return _rareBoxGemPrice;
            case GemProductType.EpicBox: return _epicBoxGemPrice;
        }

        return 0;
    }

    public void HandleGemPurchase(GemProductType productType)
    {
        switch (productType)
        {
            case GemProductType.RareBox:
                Saves.AddBoxes(RarityType.Rare, 1);
                break;

            case GemProductType.EpicBox:
                Saves.AddBoxes(RarityType.Epic, 1);
                break;
        }
    }




}

public static partial class Configs
{
    public static ShopConfig Shop => Resources.Load<ShopConfig>($"Shop");
}