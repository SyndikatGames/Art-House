using UnityEngine;
using VG2;

public class CollectBoxes_TutorialStep : TutorialStep
{
    [SerializeField] private GameObject _collectPrompt;


    public override void RestoreContext()
    {
        _collectPrompt.SetActive(false);
        Events.onBoxesCollected -= OnBoxesCollected;
    }

    public override void Run()
    {
        _collectPrompt.SetActive(true);
        Events.onBoxesCollected += OnBoxesCollected;
    }

    private void OnBoxesCollected() => StepCompleted();



}
