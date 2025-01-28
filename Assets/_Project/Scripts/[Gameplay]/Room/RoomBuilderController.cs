using System;
using R3;
using UnityEngine.Tilemaps;
using VG2;
using Zenject;

public class RoomBuilderController : IDisposable, IInitializable
{
    private Item _rootItem;
    private Tilemap _tilemap;

    private CompositeDisposable _disposables = new CompositeDisposable();


    public RoomBuilderController(Item rootItem, Tilemap tilemap)
    {
        _rootItem = rootItem;
        _tilemap = tilemap;
    }


    public void Initialize()
    {
        var itemPlacingHandler = new RoomItemPlacingHandler(_rootItem);
        var tilemapDrawHandler = new RoomTilemapDrawHandler(_tilemap);
        var expansionHandler = new RoomExpansionHandler(_rootItem);

        expansionHandler.UpdateRoomSize();
        itemPlacingHandler.PlaceItemsFromGameState();
        tilemapDrawHandler.Draw();

        _disposables.Add(GameState.CurrentRoom.expansionLevel.Subscribe(_ =>
        {
            expansionHandler.UpdateRoomSize();
            tilemapDrawHandler.Draw();
        }));

        _disposables.Add(GameState.CurrentRoom.currentStyleIndex.Subscribe(_ => 
        {
            tilemapDrawHandler.Draw();
        }));

    }


    public void Dispose() => _disposables.Dispose();
}
