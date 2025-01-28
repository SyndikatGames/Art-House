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
        int roomLevel = PrestigeCalculator.GetCurrentRoomLevel();

        if (roomLevel >= TutorialController.SHOW_ROOM_STYLES_LEVEL)
            OnNewLevelReached(roomLevel);

        else Disposables.Add(_eventController.OnNewLevelReached.Subscribe(level => OnNewLevelReached(level)));
    }


    private void OnNewLevelReached(int level)
    {
        if (level >= TutorialController.SHOW_ROOM_STYLES_LEVEL)
        {
            var prompt = Object.Instantiate(Dependencies.LeftArrowPromptPrefab, UI.Canvas);
            prompt.text = "Измени стиль\nкомнаты";
            _prompt = prompt.gameObject;

            _roomStyleController.OnRoomStylesScreenOpened.Subscribe(screen => OnRoomStylesScreenOpened(screen));
        }
    }

    private void OnRoomStylesScreenOpened(RoomStylesScreenView screen)
    {
        Disposables.Add(_roomStyleController.OnStyleIndexChanged.Subscribe(index => OnStyleIndexChanged(index)));

        Object.Destroy(_prompt);

        _prompt = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, UI.Canvas);
        _prompt.transform.position = screen.StyleVariants[1].transform.position;
    }

    private void OnStyleIndexChanged(int index) => StepCompleted();
}
