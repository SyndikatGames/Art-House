using UnityEngine;
using UnityEngine.Tilemaps;
using VG2;
using R3;

public class RoomDrawer : ReactiveView
{
    [SerializeField] private Tilemap _tilemap;

    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.currentStyleIndex.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        int styleIndex = GameState.CurrentRoom.currentStyleIndex.Value;
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

        Vector2Int size = GameState.CurrentRoom.size.Value;

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
