using System;
using R3;
using VG2;

public class EventController : IDisposable
{
    public Observable<int> OnNewLevelReached => _onNewLevelReached; private Subject<int> _onNewLevelReached = new Subject<int>();
    public Observable<Item> OnItemPlacedFromCard => _onItemPlacedFromCard; private Subject<Item> _onItemPlacedFromCard = new Subject<Item>();

    public Subject<Unit> OnItemsMerged { get; private set; } = new Subject<Unit>();
    public Subject<Item> OnItemRotated { get; private set; } = new Subject<Item>();
    public Subject<Item> OnItemPlaced { get; private set; } = new Subject<Item>();
    public Subject<Item> OnItemTaken { get; private set; } = new Subject<Item>();
    public Subject<Unit> OnCardTaken { get; private set; } = new Subject<Unit>();
    public Subject<Unit> OnCardReleased { get; private set; } = new Subject<Unit>();


    private CompositeDisposable _disposables = new CompositeDisposable();
    private int _previousRoomLevel;
    private bool _itemTakenFromCard = false;

    public EventController()
    {
        int roomIndex = GameState.currentRoomIndex.Value;

        _disposables.Add(GameState.CurrentRoom.placedItemsHierarchy.onChanged
            .Subscribe(_ => OnPlacedItemsChanged()));

        _previousRoomLevel = PrestigeCalculator.GetCurrentRoomLevel();


        _disposables.Add(OnCardTaken.Subscribe(_ => _OnCardTaken()));
        _disposables.Add(OnItemTaken.Subscribe(_ => _OnItemTaken()));
        _disposables.Add(OnItemPlaced.Subscribe(item => _OnItemPlaced(item)));


    }

    private void _OnItemPlaced(Item item)
    {
        if (_itemTakenFromCard) _onItemPlacedFromCard.OnNext(item);
    }

    private void _OnItemTaken()
    {
        _itemTakenFromCard = false;
    }

    private void _OnCardTaken()
    {
        _itemTakenFromCard = true;
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
