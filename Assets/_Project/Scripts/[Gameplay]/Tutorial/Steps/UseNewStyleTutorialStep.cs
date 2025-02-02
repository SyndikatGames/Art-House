using System.Threading.Tasks;
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
        TaskController.SetTask(5);

        int roomLevel = PrestigeCalculator.GetCurrentRoomLevel();

        if (roomLevel >= TutorialController.SHOW_ROOM_STYLES_LEVEL)
            OnNewLevelReached(roomLevel);

        else Disposables.Add(_eventController.OnNewLevelReached.Subscribe(level => OnNewLevelReached(level)));
    }


    private async void OnNewLevelReached(int level)
    {
        if (level >= TutorialController.SHOW_ROOM_STYLES_LEVEL)
        {
            Dependencies.StylesButtonRect.gameObject.SetActive(true);

            await Task.Delay(100);

            var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, UI.Canvas);
            prompt.text = Localization.GetString("change_room_style_tutorial");
            prompt.rectTransform.SetSiblingIndex(0);
            _prompt = prompt.gameObject;

            _prompt.transform.position = Dependencies.StylesButtonRect.position;

            _roomStyleController.OnRoomStylesScreenOpened.Subscribe(screen => OnRoomStylesScreenOpened(screen));
        }
    }

    private async void OnRoomStylesScreenOpened(RoomStylesScreenView screen)
    {
        Disposables.Add(_roomStyleController.OnStyleIndexChanged.Subscribe(index => OnStyleIndexChanged(index)));

        Object.Destroy(_prompt);

        await Task.Delay(100);

        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, UI.Canvas);
        _prompt.transform.position = screen.StyleVariants[1].transform.position;
    }

    private void OnStyleIndexChanged(int index) => StepCompleted();
}
