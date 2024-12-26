using System.Collections.Generic;
using R3;
using UnityEngine;
using VG2;
using Zenject;

public class TutorialUnlocker : ReactiveView
{
    [System.Serializable]
    private struct UnlockStep
    {
        public int step;
        public GameObject unlockable;
    }

    [SerializeField] private List<UnlockStep> _unlocks;

    protected override void Subscribe()
    {
        disposables.Add(GameState.tutorialStep.Subscribe(_ => Display()));
    }


    protected override void Display()
    {
        int currentStep = GameState.tutorialStep.Value;

        foreach (var unlockStep in _unlocks)
            unlockStep.unlockable.SetActive(currentStep >= unlockStep.step);


    }
}
