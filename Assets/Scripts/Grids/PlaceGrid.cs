using UnityEngine;

[System.Serializable]
public class PlaceGrid : IsometricGrid
{
    public PlaceGrid(PlaceGridData data, Vector3Int position, Side side)
    {
        gridType = data.gridType;
        size = data.size;

        Vector3Int offset = side == Side.Left ?
            data.offset : new Vector3Int(data.offset.y, data.offset.x, data.offset.z);

        base.position = position + offset;
    }


    public bool TryPlaceItem(Item item, Vector2 position, 
        out Vector2 resultPosition, out Vector3Int resultGridPosition)
    {
        resultPosition = default;
        resultGridPosition = default;
        if (CheckPlaceType(item) == false) return false;

        CartesianToIsometric(position, out var localPosition, out var worldPosition);
        resultGridPosition = base.position + worldPosition;

        if (CheckInsideGrid(item, localPosition) && 
            ItemList.CheckNoIntersetion(resultGridPosition, item.Size))
        {
            resultPosition = CellToWorld(localPosition);

            if (gridType == GridType.ToLeft) item.SetSide(Side.Left);
            else if (gridType == GridType.ToRight) item.SetSide(Side.Right);

            return true;
        }

        return false;
    }

    private bool CheckPlaceType(Item item) =>
        item.PlaceType == ItemPlaceType.Floor && gridType == GridType.Floor ||
        item.PlaceType == ItemPlaceType.Wall && gridType == GridType.ToLeft ||
        item.PlaceType == ItemPlaceType.Wall && gridType == GridType.ToRight;

    private bool CheckInsideGrid(Item item, Vector2Int gridPosition)
    {
        Vector2Int itemSize = default;

        switch (gridType)
        {
            case GridType.Floor:
                itemSize = new Vector2Int(item.Size.x, item.Size.y);
                break;

            case GridType.ToLeft:
                itemSize = new Vector2Int(item.Size.y, item.Size.z);
                break;

            case GridType.ToRight:
                itemSize = new Vector2Int(item.Size.x, item.Size.z);
                break;
        }

        return gridPosition.x >= 0 && gridPosition.y >= 0 &&
            size.x >= gridPosition.x + itemSize.x &&
            size.y >= gridPosition.y + itemSize.y;
    }




}
