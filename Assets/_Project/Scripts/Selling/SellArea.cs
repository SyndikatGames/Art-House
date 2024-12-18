using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VG;

public class SellArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    //[SerializeField] private SellParticle _sellParticlePrefab;


    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Enter");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("Exit");
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("drop");

        if (ItemMoveHandler.DraggableItem != null)
            SellItem(ItemMoveHandler.DraggableItem, eventData.position);
    }


    private void SellItem(Item item, Vector2 screenPosition)
    {
        float sellPrice = TotalRules.GetItemSellPrice(item.RarityType);

        item.GetComponent<ItemInteraction>().CardMoveHandler.IsSold = true;

        //var sellParticle = Instantiate(_sellParticlePrefab, UI.Canvas);
        //sellParticle.GetComponent<RectTransform>().position = screenPosition;
        //sellParticle.SetSellPrice(sellPrice);

        Saves.Float[Key_Save.soft_money].Value += sellPrice;
        Sound.Play(Key_Sound.SellItem);
    }



}
