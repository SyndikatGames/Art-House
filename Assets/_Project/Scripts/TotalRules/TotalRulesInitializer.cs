using System.Collections;
using UnityEngine;
using VG;

public class TotalRulesInitializer : Initializable
{
    public override void Initialize()
    {
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        yield return new WaitUntil(() => Saves.Initialized);

        Saves.Int[Key_Save.current_room_index].onChanged += () => TotalRules.Update();

        for (int i = 0; i < Saves.roomsAmount; i++)
            Saves.String[Key_Save.room_data(i)].onChanged += () => TotalRules.Update();

        TotalRules.Update();

        InitCompleted();
    }




}
