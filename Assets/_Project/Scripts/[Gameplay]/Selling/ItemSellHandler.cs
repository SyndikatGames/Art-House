using UnityEngine;
using VG2;

public class ItemSellHandler
{
    private Item _item;


    public ItemSellHandler(Item item)
    {
        _item = item;
    }


    public void ShowSellArea() => SellArea.SetActive(true);
    public void HideSellArea() => SellArea.SetActive(false);

    public bool TrySellItem(Vector2 screenPosition)
    {
        if (SellArea.PointerInsideArea == false) return false;

        float sellPrice = ConfigHub.BaseValues.GetItemSellPrice(_item.RarityType);
        _item.Destroy();

        var sellParticlePrefab = Resources.Load<SellParticle>("Prefabs/SellParticle");

        var sellParticle = Object.Instantiate(sellParticlePrefab, UI.Canvas);
        sellParticle.GetComponent<RectTransform>().position = screenPosition;
        sellParticle.SetSellPrice(sellPrice);
        sellParticle.RunAnimation();

        CardCalculator.RemoveCard(_item.GetCardModel());
        GameState.money.Value += sellPrice;
        return true;
    }




}
