using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using VG2;

public class ReactTest : ReactiveView
{
    protected override void Subscribe()
    {
        var state = new GameState();
        disposables.Add(state.adsEnabled.Subscribe(_ => Display()));
        disposables.Add(state.roomStates[0].cards.onChanged.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        
    }

    
}
