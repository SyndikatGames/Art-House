using UnityEngine;

[System.Serializable]
public class PlaceGrid : IsometricGrid
{
    public PlaceGrid(PlaceGridData data, Vector3Int position)
    {
        gridType = data.gridType;
        size = data.size;
        base.position = position + data.offset;
    }


    public bool TryPlaceItem(Item item, Vector2 position, out Vector3Int resultGridPosition)
    {
        resultGridPosition = default;
        if (CheckPlaceType(item) == false) return false;

        CartesianToIsometric(position, out var localPosition, out var worldPosition);
        resultGridPosition = base.position + worldPosition;

        if (CheckInsideGrid(item.Size, localPosition) && 
            ItemList.CheckNoIntersetion(resultGridPosition, item.Size))
        {
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

    private bool CheckInsideGrid(Vector3Int size, Vector2Int gridPosition)
    {
        Vector2Int itemSize = default;

        switch (gridType)
        {
            case GridType.Floor:
                itemSize = new Vector2Int(size.x, size.y);
                break;

            case GridType.ToLeft:
                itemSize = new Vector2Int(size.y, size.z);
                break;

            case GridType.ToRight:
                itemSize = new Vector2Int(size.x, size.z);
                break;
        }

        return gridPosition.x >= 0 && gridPosition.y >= 0 &&
            base.size.x >= gridPosition.x + itemSize.x &&
            base.size.y >= gridPosition.y + itemSize.y;
    }




}
