using System;
using System.Collections.Generic;
using System.Collections;
using TopologicalSorting;
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
        DependencyGraph dependencyGraph = new DependencyGraph();
        List<OrderedProcess> orders = new List<OrderedProcess>(PlacedItems.Count);
        for (int i = 0; i < PlacedItems.Count; i++)
            orders.Add(new OrderedProcess(dependencyGraph, i.ToString()));

        for (int i = 0; i < PlacedItems.Count; i++)
        {
            var item = PlacedItems[i];

            for (int j = i + 1; j < PlacedItems.Count; j++)
            {
                var otherItem = PlacedItems[j];
                bool sortIsMatter = 
                    item.ItemType == ItemType.Room || 
                    otherItem.ItemType == ItemType.Room || 
                    item.SpriteBounds.Intersects(otherItem.SpriteBounds);

                if (sortIsMatter)
                {
                    Vector3Int itemFrontPosition = item.Position + item.Size;

                    bool currentItemIsBehind =
                        itemFrontPosition.x <= otherItem.Position.x ||
                        itemFrontPosition.y <= otherItem.Position.y ||
                        itemFrontPosition.z <= otherItem.Position.z;

                    if (currentItemIsBehind) orders[i].Before(orders[j]);
                    else orders[i].After(orders[j]);
                }
            }

        }

        try
        {
            var sortedEnumerator = dependencyGraph.CalculateSort().GetEnumerator();

            for (int order = 0; sortedEnumerator.MoveNext(); order++)
            {
                OrderedProcess orderedProcess = (OrderedProcess)sortedEnumerator.Current;
                int index = int.Parse(orderedProcess.Name);

                PlacedItems[index].SortingOrder = order;
            }
        }
        catch { }
        

        


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
