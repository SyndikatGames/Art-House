using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public static List<Item> Items { get; private set; }


    private void Awake()
    {
        Items = new List<Item>();
    }

    public static void ResortSprites()
    {
        var orderItemList = new List<Item>();


        for (int i = 0; i < Items.Count; i++)
            Items[i].SortingOrder = 0;

        for (int i = 0; i < Items.Count; i++)
        {
            var item = Items[i];
            Vector3Int itemFrontPosition = item.Position + item.Size;

            for (int j = i + 1; j < Items.Count; j++)
            {
                var otherItem = Items[j];

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
        for (int i = 0; i < Items.Count; i++)
        {
            if (CheckCubeIntersection(position, size,
                Items[i].Position, Items[i].Size))
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
