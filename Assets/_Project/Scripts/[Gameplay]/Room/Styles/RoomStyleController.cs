using System;
using R3;
using VG2;

public class RoomStyleController : IDisposable
{

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


    public void OpenRoomStylesWindow()
    {
        SceneContainer.InstantiatePrefabFromComponent(ConfigHub.RoomStyles.RoomStylesScreenPrefab, UI.Canvas);
        
    }

    public void SetRoomStyle(int roomStyleIndex)
    {
        GameState.CurrentRoom.currentStyleIndex.Value = roomStyleIndex;
        GameState.CurrentRoom.newStyleIndices.Remove(roomStyleIndex);
    }





}
