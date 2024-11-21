using System;
using System.Collections.Generic;
using UnityEngine;
using VG;

public static class ItemList
{
    public static event Action onUpdated;

    public static List<Item> PlacedItems { get; private set; } = new List<Item>();


    public static void UpdateItems()
    {
        Saves.SetRoomItems(roomIndex: 0, PlacedItems);
        onUpdated?.Invoke();
    }


    public static void ResortOrder()
    {
        var itemSortedList = new List<Item>(PlacedItems.Count); 

        for (int i = 0; i < PlacedItems.Count; i++)
        {
            var currentItem = PlacedItems[i];
            Vector3Int itemFrontPosition = currentItem.Position + currentItem.Size;

            bool itemSorted = false;
            for (int j = itemSortedList.Count - 1; j >= 0; j--)
            {
                var otherItem = itemSortedList[j];

                bool currentItemIsBehind = 
                    itemFrontPosition.x <= otherItem.Position.x ||
                    itemFrontPosition.y <= otherItem.Position.y ||
                    itemFrontPosition.z <= otherItem.Position.z;

                if (!currentItemIsBehind)
                {
                    Debug.Log($"{currentItem.name}: {itemFrontPosition} front {otherItem}: {otherItem.Position}");

                    if (j == itemSortedList.Count - 1)
                        itemSortedList.Add(currentItem);

                    else itemSortedList.Insert(j + 1, currentItem);

                    itemSorted = true;
                    break;
                }   
            }

            if (!itemSorted)
            {
                itemSortedList.Add(currentItem);
                Debug.Log($"{currentItem.name} front");
            }
        }

        for (int i = 0; i < itemSortedList.Count; i++)
            itemSortedList[i].SortingOrder = i;
            

    }


    public static bool CheckNoIntersetion(Vector3Int position, Vector3Int size)
    {
        for (int i = 0; i < PlacedItems.Count; i++)
        {
            if (CheckCubeIntersection(position, size,
                PlacedItems[i].Position, PlacedItems[i].Size))
                return false;
        }

        return true;
    }

    private static bool CheckCubeIntersection(
        Vector3Int rectPosition1, Vector3Int rectSize1,
        Vector3Int rectPosition2, Vector3Int rectSize2)
    {
        int rightX1 = rectPosition1.x + rectSize1.x;
        int bottomY1 = rectPosition1.y + rectSize1.y;
        int forwardZ1 = rectPosition1.z + rectSize1.z;

        int rightX2 = rectPosition2.x + rectSize2.x;
        int bottomY2 = rectPosition2.y + rectSize2.y;
        int forwardZ2 = rectPosition2.z + rectSize2.z;

        bool noIntersection = 
            rightX1 <= rectPosition2.x || 
            rightX2 <= rectPosition1.x ||
            bottomY1 <= rectPosition2.y || 
            bottomY2 <= rectPosition1.y ||
            forwardZ1 <= rectPosition2.z ||
            forwardZ2 <= rectPosition1.z;

        return !noIntersection;
    }

}
