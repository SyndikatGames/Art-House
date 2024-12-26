using System.Collections.Generic;

public static class ItemPlacingCalculator
{
    
    public static List<PlacedItemModel> ConvertToPlacedItemsHierarchy(List<Item> placedItems)
    {
        var result = new List<PlacedItemModel>();
        foreach (var placedItem in placedItems)
            if ((placedItem.ParentItem == null || placedItem.ParentItem.ItemType == ItemType.Room)
                && placedItem.ItemType != ItemType.Room)
                result.Add(GetPlacedItemModelRecursive(placedItem));

        return result;
    }

    private static PlacedItemModel GetPlacedItemModelRecursive(Item item)
    {
        var model = new PlacedItemModel();
        model.position = item.Position;
        model.rarityType = item.RarityType;
        model.itemType = item.ItemType;
        model.side = item.CurrentSide;

        model.childItems = new List<PlacedItemModel>();
        if (item.ChildItems != null)
            foreach (var childItem in item.ChildItems)
                model.childItems.Add(GetPlacedItemModelRecursive(childItem));
        
        return model;
    }
    



}
