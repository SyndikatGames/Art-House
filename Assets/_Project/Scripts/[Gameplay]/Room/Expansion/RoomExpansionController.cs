using R3;
using UnityEngine;
using VG2;

public class RoomExpansionController
{
    public Observable<Unit> OnRoomExpanded => _onRoomExpanded;
    private Subject<Unit> _onRoomExpanded = new();

    public Observable<RoomExpansionWindowView> OnOpenWindow => _onOpenWindow;
    private Subject<RoomExpansionWindowView> _onOpenWindow = new();



    private RoomExpansionWindowView _window;


    
    public void OpenRoomExpansionWindow()
    {
        _window = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Room.RoomExpansionWindowPrefab, UI.Canvas);
        _onOpenWindow.OnNext(_window);
    }

    public void BuyNextRoomExpansion()
    {
        float price = ConfigHub.BaseValues.GetRoomExpansionPrice(GameState.CurrentRoom.expansionLevel.Value);

        if (GameState.money.Value >= price)
        {
            GameState.money.Value -= price;
            GameState.CurrentRoom.expansionLevel.Value++;
            Object.Destroy(_window.gameObject);
            _onRoomExpanded.OnNext(Unit.Default);
        }
    }



}
