using UnityEngine;




[System.Serializable]
public class PlaceGrid : MonoBehaviour
{
    [SerializeField] private IsometricGrid _grid;


    public bool TryPlacingItem(Item item, Vector2 position, 
        out Vector2 resultPosition, out PlaceGrid grid)
    {
        resultPosition = default;
        grid = this;
        if (CheckPlaceType(item) == false) return false;

        var gridPosition = _grid.WorldToCell(position);

        if (CheckInsideGrid(item, gridPosition))
        {
            resultPosition = _grid.CellToWorld(gridPosition);
            Debug.Log($"Placed on {_grid.Type}, {gridPosition}");
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
        && _grid.Size.x >= gridPosition.x + item.Size.x 
        && _grid.Size.y >= gridPosition.y + item.Size.y;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        GridGizmosDrawer.Draw(transform.position, _grid.Type, _grid.Size);
    }


}
