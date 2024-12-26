using System.Collections.Generic;
using UnityEngine;
using VG2;
using R3;
using Zenject;

public class StylesWindow : ReactiveView
{
    public static string Name => "Styles(Clone)";

    [SerializeField] private List<StyleVariant> _styleVariants;

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
    public static StylesWindow StylesWindow
        => Resources.Load<StylesWindow>("Windows/Styles");
}
