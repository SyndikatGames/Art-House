using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VG2;

public class UseNewStyleTutorialStep : TutorialStep
{
    private EventController _eventController;
    private RoomStyleController _roomStyleController;
    private GameObject _prompt;


    public UseNewStyleTutorialStep(EventController eventController, RoomStyleController roomStyleController)
    {
        _eventController = eventController;
        _roomStyleController = roomStyleController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        Object.Destroy(_prompt);
    }


    public override void Run()
    {
        var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, UI.Canvas);
        prompt.Text = Localization.GetString("change_room_style_tutorial");
        prompt.transform.GetComponent<RectTransform>().SetSiblingIndex(0);
        _prompt = prompt.gameObject;

        _prompt.transform.position = Dependencies.StylesButtonRect.position;

        _roomStyleController.OnRoomStylesScreenOpened.Subscribe(screen => OnRoomStylesScreenOpened(screen));
    }

    private async void OnRoomStylesScreenOpened(RoomStylesScreenView screen)
    {
        Disposables.Add(GameState.CurrentRoom.currentStyleIndex.Subscribe(index => OnStyleIndexChanged(index)));

        Object.Destroy(_prompt);

        await UniTask.Yield();

        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, UI.Canvas);
        _prompt.transform.position = screen.StyleVariants[1].transform.position;
    }

    private void OnStyleIndexChanged(int index) => StepCompleted();
}
