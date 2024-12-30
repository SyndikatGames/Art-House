using R3;
using VG2;
using Zenject;

public class ShowNewRoomLevelWindowEvent : ReactiveEvent
{
    [Inject] private EventController _eventController;


    protected override void Subscribe()
    {
        disposables.Add(_eventController.OnNewLevelReached.Subscribe(_ => OnNewLevelReached()));
    }

    private void OnNewLevelReached()
    {
        Instantiate(Prefabs.RoomWindow, UI.Canvas).OpenNewLevel();
    }

}
