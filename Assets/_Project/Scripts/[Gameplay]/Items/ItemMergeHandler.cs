using UnityEngine;

public class ItemMergeHandler
{
    private Item _item;


    public ItemMergeHandler(Item item)
    {
        _item = item;
    }


    public bool OnDrop()
    {
        if (ItemMoveHandler.DraggableItem != null && MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            ItemPlacing.PlacedItems.Remove(ItemMoveHandler.DraggableItem);
            Object.Destroy(ItemMoveHandler.DraggableItem.gameObject);

            RarityType newRarity = (RarityType)((int)_item.RarityType + 1);
            _item.SetRarity(newRarity);
            _item.Effects.MergingFlash();

            ItemPlacing.UpdateItems();
            Events.ItemsMerged();
            return true;
        }

        return false;

    }

    public void OnPointerExit()
    {
        if (ItemMoveHandler.DraggableItem == null) return;

        if (MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            _item.Effects.TargetItemMergingHighlight();
            SetMergingHiglightForEveryItem(true);
        }


    }

    public void OnPointerEnter()
    {
        if (ItemMoveHandler.DraggableItem == null) return;

        if (MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            SetMergingHiglightForEveryItem(false);
            _item.Effects.TargetItemMergingHighlight();
        }
    }

    public void OnEndDrag()
    {
        SetMergingHiglightForEveryItem(false);
    }

    public void OnBeginDrag()
    {
        if (_item.ChildItems.Count > 0) return;
        SetMergingHiglightForEveryItem(true);
    }




    private void SetMergingHiglightForEveryItem(bool value)
    {
        foreach (var item in ItemPlacing.PlacedItems)
        {
            if (MergingAvailable(item, _item))
            {
                if (value) item.Effects.MergingAvailableHighlight();
                else item.Effects.DisableCurrentEffect();
            }
                
        }
    }


    private bool MergingAvailable(Item item1, Item item2)
    {
        if (item1 == item2) return false;

        if (item1.RarityType == item2.RarityType 
            && item1.ItemType == item2.ItemType
            && item1.RarityType != RarityType.Legendary)
        {
            RarityType nextRarityType = (RarityType)((int)item1.RarityType + 1);
            return item1.RarityExists(nextRarityType);
        }

        return false;
    }


}
