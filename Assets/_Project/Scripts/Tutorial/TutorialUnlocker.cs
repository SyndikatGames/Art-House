using System.Collections.Generic;
using UnityEngine;
using VG;

public class TutorialUnlocker : Info
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
        Saves.Int[Key_Save.tutorial_step].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.Int[Key_Save.tutorial_step].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
    {
        int currentStep = Saves.Int[Key_Save.tutorial_step].Value;

        foreach (var unlockStep in _unlocks)
            unlockStep.unlockable.SetActive(currentStep >= unlockStep.step);


    }
}
