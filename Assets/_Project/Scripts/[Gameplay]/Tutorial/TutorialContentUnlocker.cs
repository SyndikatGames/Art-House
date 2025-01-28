using System.Collections.Generic;
using UnityEngine;
using VG2;
using R3;

public class TutorialContentUnlocker : ReactiveView
{
    [System.Serializable]
    private struct ContentUnlock
    {
        public int stepIndex;
        public GameObject gameObject;
    }

    [SerializeField] private List<ContentUnlock> _contentUnlocks;




    protected override void Subscribe()
    {
        if (GameState.tutorialCompleted == false)
            disposables.Add(GameState.tutorialStepIndex.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        int stepIndex = GameState.tutorialStepIndex.Value;

        foreach (var item in _contentUnlocks)
            item.gameObject.SetActive(item.stepIndex <= stepIndex);
            

    }


    
    





}
