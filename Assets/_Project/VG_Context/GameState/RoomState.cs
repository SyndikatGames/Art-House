using R3;
using VG2;

public struct RoomState
{
    public ReactiveList<int> newStyleIndices;

    public ReactiveProperty<int> currentStyleIndex;

    public ReactiveDictionary<BoxType, int> boxesAmount;

    public ReactiveProperty<float> accumulatedMoney;

    public ReactiveList<PlacedItemModel> placedItemsHierarchy;

    public ReactiveList<CardModel> cards;

}
