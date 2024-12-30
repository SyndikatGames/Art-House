using System.Collections.Generic;
using R3;
using UnityEngine;
using VG2;

public class RoomStylesScreenView : ReactiveView
{

    [SerializeField] private List<RoomStyleVariantView> _styleVariants;

    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.currentStyleIndex.Subscribe(_ => Display()));
        disposables.Add(GameState.CurrentRoom.newStyleIndices.onChanged.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        for (int i = 0; i < _styleVariants.Count; i++)
            _styleVariants[i].UpdateStyleIndex(i);

    }


}

public static partial class Prefabs
{
    public static RoomStylesScreenView StylesWindow
        => Resources.Load<RoomStylesScreenView>("Windows/Styles");
}
