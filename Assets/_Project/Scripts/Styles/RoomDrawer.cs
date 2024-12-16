using UnityEngine;
using UnityEngine.Tilemaps;
using VG;

public class RoomDrawer : Info
{
    [SerializeField] private Tilemap _tilemap;

    protected override void Subscribe()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged += UpdateValue;
        Saves.String[Key_Save.room_size_data(0)].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged -= UpdateValue;
        Saves.String[Key_Save.room_size_data(0)].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
    {
        int styleIndex = Saves.Int[Key_Save.current_style_index(0)].Value;
        var styleData = Configs.GetStyles(0).GetStyle(styleIndex);

        Tile floorTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        floorTile.sprite = styleData.floorSprite;
        floorTile.color = Color.white;

        Tile toRightWallTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        toRightWallTile.sprite = styleData.toRightWallSprite;
        toRightWallTile.color = Color.white;

        Tile toLeftWallTile = (Tile)ScriptableObject.CreateInstance(typeof(Tile));
        toLeftWallTile.sprite = styleData.toLeftWallSprite;
        toLeftWallTile.color = Color.white;

        Vector2Int size = Saves.RoomSize;

        for (int x = 1; x <= size.x; x++)
            _tilemap.SetTile(new Vector3Int(-x, 0), toRightWallTile);

        for (int y = 1; y <= size.y; y++)
            _tilemap.SetTile(new Vector3Int(0, -y), toLeftWallTile);

        for (int x = 1; x <= size.x; x++)
            for (int y = 1; y <= size.y; y++)
                _tilemap.SetTile(new Vector3Int(-x, -y), floorTile);

        _tilemap.RefreshAllTiles();
    }
}
