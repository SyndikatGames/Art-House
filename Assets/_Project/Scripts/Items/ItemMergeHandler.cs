using UnityEngine;

public class ItemMergeHandler
{
    [SerializeField] private Item _item;

    public ItemMergeHandler(Item item)
    {
        _item = item;
    }


    public bool OnDrop()
    {
        if (ItemMoveHandler.DraggableItem != null && MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            ItemList.PlacedItems.Remove(ItemMoveHandler.DraggableItem);
            Object.Destroy(ItemMoveHandler.DraggableItem.gameObject);

            RarityType newRarity = (RarityType)((int)_item.RarityType + 1);
            _item.SetRarity(newRarity);
            _item.Highlighter.Stop(HighlightType.MergingSelect);

            ItemList.UpdateItems();
            return true;
        }

        return false;

    }

    public void OnPointerExit()
    {
        if (ItemMoveHandler.DraggableItem == null) return;

        if (MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            _item.Highlighter.Stop(HighlightType.MergingSelect);
            SetMergingHiglight(true);
        }


    }

    public void OnPointerEnter()
    {
        if (ItemMoveHandler.DraggableItem == null) return;

        if (MergingAvailable(ItemMoveHandler.DraggableItem, _item))
        {
            SetMergingHiglight(false);
            _item.Highlighter.Highlight(HighlightType.MergingSelect);
        }
    }

    public void OnEndDrag()
    {
        SetMergingHiglight(false);
    }

    public void OnBeginDrag()
    {
        if (_item.ChildItems.Count > 0) return;
        SetMergingHiglight(true);
    }




    private void SetMergingHiglight(bool value)
    {
        foreach (var item in ItemList.PlacedItems)
        {
            if (MergingAvailable(item, _item))
            {
                if (value) item.Highlighter.Highlight(HighlightType.MergingAvailable);
                else item.Highlighter.Stop(HighlightType.MergingAvailable);
            }
                
        }
    }


    private bool MergingAvailable(Item item1, Item item2)
        => item1.RarityType == item2.RarityType && item1.ItemType == item2.ItemType
        && item1.RarityType != RarityType.Legendary;

}
