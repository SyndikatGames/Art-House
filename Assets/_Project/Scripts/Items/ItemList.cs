using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static List<Item> PlacedItems { get; private set; }


    private void Awake()
    {
        PlacedItems = new List<Item>();
    }

    public static void ResortOrder()
    {
        for (int i = 0; i < PlacedItems.Count; i++)
            PlacedItems[i].SortingOrder = 0;

        for (int i = 0; i < PlacedItems.Count; i++)
        {
            var item = PlacedItems[i];
            Vector3Int itemFrontPosition = item.Position + item.Size;

            for (int j = 0; j < PlacedItems.Count; j++)
            {
                if (j == i) continue;

                var otherItem = PlacedItems[j];

                if (itemFrontPosition.x <= otherItem.Position.x ||
                    itemFrontPosition.y <= otherItem.Position.y ||
                    itemFrontPosition.z <= otherItem.Position.z)
                    otherItem.SortingOrder++;

                else item.SortingOrder++;
            }
        }
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
