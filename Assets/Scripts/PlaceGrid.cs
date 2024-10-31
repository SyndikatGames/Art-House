using System.Collections.Generic;
using UnityEngine;




[System.Serializable]
public class PlaceGrid : MonoBehaviour
{
    [SerializeField] private IsometricGrid _grid;
    private Dictionary<Item, Vector2Int> _placedItems = new Dictionary<Item, Vector2Int>();

    public void PlaceItem(Item item, Vector2 position)
        => _placedItems.Add(item, _grid.WorldToCell(position));

    public void RemoveItem(Item item)
    {
        if (_placedItems.ContainsKey(item))
            _placedItems.Remove(item);

        else
        {
            foreach (var placedItem in _placedItems)
            {
                foreach (var placeGrid in placedItem.Key.PlaceGrids)
                    placeGrid.RemoveItem(item);
            }    
        }
    }



    public bool TryPlacingItem(Item item, Vector2 position, 
        out Vector2 resultPosition, out PlaceGrid grid)
    {
        resultPosition = default;
        grid = this;
        if (CheckPlaceType(item) == false) return false;

        var gridPosition = _grid.WorldToCell(position);

        if (CheckInsideGrid(item, gridPosition) && CheckNoIntersection(item, gridPosition))
        {
            resultPosition = _grid.CellToWorld(gridPosition);
            Debug.Log($"Placed on {_grid.Type}, {gridPosition}");
            return true;
        }

        foreach (var placedItem in _placedItems)
            foreach (var placeGrid in placedItem.Key.PlaceGrids)
            {
                if (placeGrid.TryPlacingItem(item, position, out resultPosition, out grid))
                    return true;
            }

        return false;
    }

    private bool CheckPlaceType(Item item) =>
        item.PlaceType == ItemPlaceType.Floor && _grid.Type == GridType.Floor ||
        item.PlaceType == ItemPlaceType.Wall && _grid.Type == GridType.ToLeft ||
        item.PlaceType == ItemPlaceType.Wall && _grid.Type == GridType.ToRight;

    private bool CheckInsideGrid(Item item, Vector2Int gridPosition) =>
        gridPosition.x >= 0 && gridPosition.y >= 0
        && _grid.Size.x >= gridPosition.x + item.OccupiedGrid.Size.x 
        && _grid.Size.y >= gridPosition.y + item.OccupiedGrid.Size.y;

    private bool CheckNoIntersection(Item item, Vector2Int gridPosition)
    {
        foreach (var placedItem in _placedItems)
        {
            var absPlacedGridPosition = placedItem.Value;
            absPlacedGridPosition.x = Mathf.Abs(absPlacedGridPosition.x);
            absPlacedGridPosition.y = Mathf.Abs(absPlacedGridPosition.y);

            bool hasIntersection = CheckRectangleIntersection(
                rectPosition1: gridPosition,
                rectSize1: item.OccupiedGrid.Size,
                rectPosition2: absPlacedGridPosition,
                rectSize2: placedItem.Key.OccupiedGrid.Size);

            if (hasIntersection) return false;
        }

        return true;
    }


    private bool CheckRectangleIntersection(
        Vector2Int rectPosition1, Vector2Int rectSize1,
        Vector2Int rectPosition2, Vector2Int rectSize2)
    {
        int rightX1 = rectPosition1.x + rectSize1.x;
        int bottomY1 = rectPosition1.y + rectSize1.y;

        int rightX2 = rectPosition2.x + rectSize2.x;
        int bottomY2 = rectPosition2.y + rectSize2.y;

        bool noIntersection = rightX1 <= rectPosition2.x || rightX2 <= rectPosition1.x ||
            bottomY1 <= rectPosition2.y || bottomY2 <= rectPosition1.y;

        return !noIntersection;
    }


}
