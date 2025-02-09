using UnityEngine;
using R3;
using VG2;

public class ExpandRoomTutorialStep : TutorialStep
{
    private RoomExpansionController _roomExpansionController;
    private GameObject _prompt;


    public ExpandRoomTutorialStep(RoomExpansionController roomExpansionController)
    {
        _roomExpansionController = roomExpansionController;
    }

    public override void RestoreContext()
    {
        base.RestoreContext();
        Object.Destroy(_prompt);
    }



    public override void Run()
    {
        Disposables.Add(_roomExpansionController.OnOpenWindow.Subscribe(window => OnRoomExpansionOpenWindow(window)));

        TaskController.SetTask(4);

        var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, Dependencies.ExpandRoomButtonRect);
        prompt.Text = Localization.GetString("expand_room_tutorial");
        _prompt = prompt.gameObject;
    }

    private void OnRoomExpansionOpenWindow(RoomExpansionWindowView window)
    {
        Disposables.Add(_roomExpansionController.OnRoomExpanded.Subscribe(_ => OnRoomExpanded()));

        Object.Destroy(_prompt);
        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, window.BuyButtonRect);

    }

    private void OnRoomExpanded()
    {
        StepCompleted();
    }

}
