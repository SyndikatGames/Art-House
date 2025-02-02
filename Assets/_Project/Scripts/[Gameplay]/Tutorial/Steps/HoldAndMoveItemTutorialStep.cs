using R3;
using UnityEngine;
using VG2;

public class HoldAndMoveItemTutorialStep : TutorialStep
{
    private EventController _eventController;

    private GameObject _cursor;
    private GameObject _prompt;

    private Vector3Int _originItemPosition;



    public HoldAndMoveItemTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }

    private void OnItemPlaced(Item item)
    {
        if (_originItemPosition == item.Position)
        {
            _cursor.SetActive(true);
            _prompt.SetActive(true);
        }

        else StepCompleted();
    }

    private void OnItemTaken()
    {
        _cursor.SetActive(false);
        _prompt.SetActive(false);
    }


    public override void RestoreContext()
    {
        base.RestoreContext();

        if (_cursor != null) Object.Destroy(_cursor);
        if (_prompt != null) Object.Destroy(_prompt);
        RaycastBlock.Disable();
    }


    public override void Run()
    {
        Disposables.Add(_eventController.OnItemTaken.Subscribe(_ => OnItemTaken()));
        Disposables.Add(_eventController.OnItemPlaced.Subscribe(item => OnItemPlaced(item)));

        TaskController.SetTask(0);

        var cursorTween = Object.Instantiate(Dependencies.HoldAndFadeMoveCursorPrefab, UI.Canvas);
        var promptText = Object.Instantiate(Dependencies.LeftPromptPrefab, UI.Canvas);

        var placedItem = ItemPlacing.PlacedItems[1];
        _originItemPosition = placedItem.Position;

        Vector2 from = MainCamera.Camera.WorldToScreenPoint(placedItem.transform.position);
        promptText.transform.position = from;
        promptText.text = Localization.GetString("hold_and_move_item");

        Vector2 to = from + Vector2.right * 400f;

        cursorTween.Run(from, to);

        _cursor = cursorTween.gameObject;
        _prompt = promptText.gameObject;

        RaycastBlock.Concentrate(placedItem.ClickImage);
    }


}
