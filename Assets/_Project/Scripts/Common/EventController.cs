using System;
using R3;
using VG2;

public class EventController : IDisposable
{
    public Observable<int> OnNewLevelReached => _onNewLevelReached;

    private Subject<int> _onNewLevelReached = new Subject<int>();

    public Subject<Unit> OnItemsMerged { get; private set; } = new Subject<Unit>();
    public Subject<Item> OnItemRotated { get; private set; } = new Subject<Item>();
    public Subject<Item> OnItemPlaced { get; private set; } = new Subject<Item>();




    private CompositeDisposable _disposables = new CompositeDisposable();
    private int _previousRoomLevel;

    public EventController()
    {
        int roomIndex = GameState.currentRoomIndex.Value;

        _disposables.Add(GameState.CurrentRoom.placedItemsHierarchy.onChanged
            .Subscribe(_ => OnPlacedItemsChanged()));

        _previousRoomLevel = PrestigeCalculator.GetCurrentRoomLevel();
    }

    public void Dispose() => _disposables.Dispose();


    private void OnPlacedItemsChanged()
    {
        
        int currentLevel = PrestigeCalculator.GetCurrentRoomLevel();
        if (_previousRoomLevel < currentLevel)
        {
            _onNewLevelReached.OnNext(currentLevel);
            _previousRoomLevel = currentLevel;
        }
            


    }

    
}
