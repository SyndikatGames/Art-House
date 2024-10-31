using System.Collections.Generic;
using UnityEngine;

public enum ItemPlaceType { Floor, Wall }


public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    [field: SerializeField] public ItemPlaceType PlaceType { get; private set; }
    [field: SerializeField] public Vector3Int Size { get; private set; }
    [field: SerializeField] public List<PlaceGrid> PlaceGrids { get; private set; }
    
    public Vector3Int Position { get; private set; }


    public void Place(Vector3Int position)
    {
        Position = position;
        ItemList.Add(this);
    }


    public void SetTransparent(bool value)
    {
        var color = _sprite.color;
        color.a = value ? 0.5f : 1f;
        _sprite.color = color;
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
    }



}
