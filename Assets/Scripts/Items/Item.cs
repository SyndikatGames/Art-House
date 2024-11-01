using System.Collections.Generic;
using UnityEngine;

public enum ItemPlaceType { Floor, Wall }
public enum Side { Left, Right }


public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    [field: SerializeField] public ItemPlaceType PlaceType { get; private set; }
    [field: SerializeField] public Vector3Int Size { get; private set; }
    [SerializeField] private List<PlaceGridData> _placeGridDataList = new List<PlaceGridData>();
    
    public Vector3Int Position { get; private set; }
    public List<PlaceGrid> PlaceGrids { get; private set; }

    private Side _currentSide = Side.Left;


    public void SetPlace(Vector3Int position)
    {
        Position = position;

        PlaceGrids = new List<PlaceGrid>(_placeGridDataList.Count);
        foreach (var placeGridData in _placeGridDataList)
            PlaceGrids.Add(new PlaceGrid(placeGridData, position, _currentSide));

        ItemList.Items.Add(this);
        ItemList.ResortSprites();
    }

    public void Placing(Vector3Int position)
    {
        Position = position;

        ItemList.Items.Add(this);
        ItemList.ResortSprites();
        ItemList.Items.Remove(this);
    }

    public void SetSide(Side side)
    {
        bool sideWasChanged = _currentSide != side;
        if (sideWasChanged)
        {
            Size = new Vector3Int(Size.y, Size.x, Size.z);
            Vector3 newScale = transform.localScale;
            newScale.x = -newScale.x;
            transform.localScale = newScale;
        }

        _currentSide = side;
    }


    public int SortingOrder
    {
        get => _sprite.sortingOrder;
        set => _sprite.sortingOrder = value;
    }

    public void SetTransparent(bool value)
    {
        var color = _sprite.color;
        color.a = value ? 0.5f : 1f;
        _sprite.color = color;
        _sprite.sortingLayerName = value ? "Front" : "Item";
    }



    private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.5f;
        Gizmos.color = color;
        
        GridGizmosDrawer.Draw(transform.position, GridType.Floor,
            new Vector2Int(Size.x, Size.y));

        GridGizmosDrawer.Draw(transform.position, GridType.ToLeft,
            new Vector2Int(Size.y, Size.z));

        GridGizmosDrawer.Draw(transform.position, GridType.ToRight,
            new Vector2Int(Size.x, Size.z));

        foreach (var placeGridData in _placeGridDataList)
        {
            Vector2 position = transform.position;
            position += 
                placeGridData.offset.x * IsometricGrid.floorAxisX +
                placeGridData.offset.y * IsometricGrid.floorAxisY +
                placeGridData.offset.z * IsometricGrid.toRightAxisY;

            Gizmos.color = Color.white;
            GridGizmosDrawer.Draw(position, placeGridData.gridType, placeGridData.size);
        }


    }



}
