using System;
using R3;
using VG2;

public class PrestigeController : IDisposable
{
    private EventController _eventController;

    private CompositeDisposable _disposables = new CompositeDisposable();


    public LevelUpRoomWindowView LevelUpRoomWindow { get; private set; }


    public PrestigeController(EventController eventController)
    {
        _disposables.Add(eventController.OnNewLevelReached.Subscribe(_ => OnNewLevelReached()));
    }


    private void OnNewLevelReached()
    {
        LevelUpRoomWindow = SceneContainer.InstantiatePrefabFromComponent
            (ConfigHub.Room.LevelUpRoomWindowPrefab, UI.Canvas);
    }

    public void Dispose() => _disposables.Dispose();


}
