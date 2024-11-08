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

        TotalRules.Update();
        InitCompleted();
    }




}
