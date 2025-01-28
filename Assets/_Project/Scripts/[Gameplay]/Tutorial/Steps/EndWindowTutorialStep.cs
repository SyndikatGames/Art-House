using UnityEngine;
using VG2;

public class EndWindowTutorialStep : TutorialStep
{
    private EndTutorialWindowView _window;


    public override void RestoreContext()
    {
        base.RestoreContext();
        _window.TakeButton.onClick.RemoveListener(OnTakeButtonClick);
        Object.Destroy(_window.gameObject);
    }



    public override void Run()
    {
        _window = SceneContainer.InstantiatePrefabFromComponent(Dependencies.EndTutorialWindowPrefab, UI.Canvas);
        _window.TakeButton.onClick.AddListener(OnTakeButtonClick);
    }

    private void OnTakeButtonClick()
    {
        StepCompleted();
    }
}
