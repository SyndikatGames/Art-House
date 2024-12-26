using System.Collections.Generic;
using UnityEngine;
using VG2;
using Zenject;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private int _finishStep;
    [SerializeField] private List<TutorialStep> _steps;


    private void Start()
    {
        if (GameState.tutorialCompleted) return;

        int currentStepIndex = GameState.tutorialStep.Value;
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
                GameState.tutorialCompleted = true;

            GameState.tutorialStep.Value++;

            if (finishedStep < _steps.Count - 1)
                RunStep(finishedStep + 1);

            else _steps[finishedStep].RestoreContext();
        };

    }


}
