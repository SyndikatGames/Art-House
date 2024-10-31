using System.Collections.Generic;
using UnityEngine;

public class GlobalGrid : MonoBehaviour
{
    private static GlobalGrid _instance;

    [SerializeField] private List<PlaceGrid> _grids;


    private void Awake()
    {
        _instance = this;
    }

    public static void RemoveItem(Item item)
    {
        foreach (var grid in _instance._grids)
            grid.RemoveItem(item);
    }


    public static bool SetPlace(Item item, Vector2 position)
    {
        foreach (var grid in _instance._grids)
            if (grid.TryPlacingItem(item, position, out var resultPosition, out var localGrid))
            {
                localGrid.PlaceItem(item, resultPosition);
                return true;
            }

        return false;
    }


    public static void Placing(Item item, Vector2 position)
    {
        foreach (var grid in _instance._grids)
        {
            if (grid.TryPlacingItem(item, position, out var resultPosition, out var localGrid))
            {
                item.SetTransparent(false);
                item.transform.position = resultPosition;
                return;
            }
        }

        item.transform.position = position;
        item.SetTransparent(true);
    }


}
