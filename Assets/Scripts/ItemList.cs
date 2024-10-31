using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    private static List<Item> _items;


    private void Awake()
    {
        _items = new List<Item>();
    }


    public static void Add(Item item) => _items.Add(item);

    public static void Remove(Item item) => _items.Remove(item);


    public static bool CheckNoIntersetion(Item item, Vector3Int position)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (CheckCubeIntersection(position, item.Size,
                _items[i].Position, _items[i].Size))
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
