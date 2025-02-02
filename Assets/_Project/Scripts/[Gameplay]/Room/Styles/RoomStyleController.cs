using System;
using System.Threading.Tasks;
using R3;
using VG2;

public class RoomStyleController : IDisposable
{
    public Observable<int> OnStyleIndexChanged => _onStyleIndexChanged;
    private Subject<int> _onStyleIndexChanged = new();


    public Observable<RoomStylesScreenView> OnRoomStylesScreenOpened => _onRoomStylesScreenOpened;
    private Subject<RoomStylesScreenView> _onRoomStylesScreenOpened = new();



    private CompositeDisposable _disposables = new CompositeDisposable();


    public RoomStyleController(EventController eventController)
    {
        _disposables.Add(eventController.OnNewLevelReached.Subscribe(level => OnNewLevelReached(level)));
    }

    public void Dispose() => _disposables.Dispose();

    private void OnNewLevelReached(int level)
    {
        GameState.CurrentRoom.newStyleIndices.Add(level - 1);
    }


    public async void OpenRoomStylesWindow()
    {
        var screen = SceneContainer.InstantiatePrefabFromComponent(ConfigHub.Room.RoomStylesScreenPrefab, UI.Canvas);

        await Task.Delay(100);
        _onRoomStylesScreenOpened.OnNext(screen);
    }

    public void SetRoomStyle(int roomStyleIndex)
    {
        GameState.CurrentRoom.currentStyleIndex.Value = roomStyleIndex;
        GameState.CurrentRoom.newStyleIndices.Remove(roomStyleIndex);

        _onStyleIndexChanged.OnNext(roomStyleIndex);
    }





}
