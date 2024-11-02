using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum ItemPlaceType { Floor, Wall }
public enum Side { Left, Right }


public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Canvas _clickCanvas;

    [field: SerializeField] public ItemPlaceType PlaceType { get; private set; }
    [field: SerializeField] public Vector3Int Size { get; private set; }
    [SerializeField] private List<PlaceGridData> _placeGridDataList = new List<PlaceGridData>();
    
    public Vector3Int Position { get; private set; }
    public List<PlaceGrid> PlaceGrids { get; private set; }

    private Side _currentSide = Side.Left; public Side CurrentSide => _currentSide;


    public void SetPlace(Vector3Int position)
    {
        Position = position;
        UpdatePlaceGrids();

        ItemList.PlacedItems.Add(this);
        ItemList.ResortOrder();
    }

    public void Placing(Vector3Int position)
    {
        Position = position;

        ItemList.PlacedItems.Add(this);
        ItemList.ResortOrder();
        ItemList.PlacedItems.Remove(this);
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

            for (int i = 0; i < _placeGridDataList.Count; i++)
                _placeGridDataList[i] = _placeGridDataList[i].GetOtherSide();

            _currentSide = side;

            UpdatePlaceGrids();
        }
    }


    public int SortingOrder
    {
        get
        {
            if (_sprite == null) return 0;
            else return _sprite.sortingOrder;
        }
        set
        {
            if (_sprite == null) return;
            _sprite.sortingOrder = value;
            _clickCanvas.sortingOrder = value;
        }
    }

    public void SetTransparent(bool value)
    {
        var color = _sprite.color;
        color.a = value ? 0.5f : 1f;
        _sprite.color = color;
        _sprite.sortingLayerName = value ? "Front" : "Item";
    }

    private void UpdatePlaceGrids()
    {
        PlaceGrids = new List<PlaceGrid>(_placeGridDataList.Count);
        foreach (var placeGridData in _placeGridDataList)
            PlaceGrids.Add(new PlaceGrid(placeGridData, Position));
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
