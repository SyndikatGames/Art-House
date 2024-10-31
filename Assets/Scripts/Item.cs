using System.Collections.Generic;
using UnityEngine;

public enum ItemPlaceType { Floor, Wall }


public class Item : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;

    [field: SerializeField] public ItemPlaceType PlaceType { get; private set; }
    [field: SerializeField] public IsometricGrid OccupiedGrid { get; private set; }
    [field: SerializeField] public List<PlaceGrid> PlaceGrids { get; private set; }


    public void SetTransparent(bool value)
    {
        var color = _sprite.color;
        color.a = value ? 0.5f : 1f;
        _sprite.color = color;
    }




}
