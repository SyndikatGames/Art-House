using System.Collections.Generic;
using UnityEngine;
using VG;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int _finishStep;
    [SerializeField] private List<TutorialStep> _steps;
    



    private void Start()
    {
        if (Saves.Bool[Key_Save.tutorial_completed].Value) return;

        int currentStepIndex = Saves.Int[Key_Save.tutorial_step].Value;
        RunStep(currentStepIndex);
    }


    private void RunStep(int stepIndex)
    {
        if (stepIndex > 0) _steps[stepIndex - 1].RestoreContext();
        _steps[stepIndex].Run();
        _steps[stepIndex].onCompleted += () =>
        {
            int finishedStep = stepIndex;

            if (finishedStep >= _finishStep)
                Saves.Bool[Key_Save.tutorial_completed].Value = true;

            Saves.Int[Key_Save.tutorial_step].Value++;

            if (finishedStep < _steps.Count - 1)
                RunStep(finishedStep + 1);

            else _steps[finishedStep].RestoreContext();
        };

    }


}
