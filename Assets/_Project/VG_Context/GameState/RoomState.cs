using R3;
using UnityEngine;
using VG2;

public class RoomState
{
    public ReactiveList<int> newStyleIndices;

    public ReactiveProperty<int> currentStyleIndex;

    public ReactiveDictionary<BoxType, int> boxesAmount;

    public ReactiveProperty<float> accumulatedMoney;

    public ReactiveList<PlacedItemModel> placedItemsHierarchy;

    public ReactiveList<CardModel> cards;

    public ReactiveProperty<Vector2Int> size;

}
