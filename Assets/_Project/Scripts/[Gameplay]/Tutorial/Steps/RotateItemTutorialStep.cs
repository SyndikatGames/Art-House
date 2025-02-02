using R3;
using UnityEngine;
using VG2;

public class RotateItemTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _cursor;
    private GameObject _prompt;


    public RotateItemTutorialStep(EventController eventController)
    {
        _eventController = eventController;
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
        Disposables.Add(_eventController.OnItemRotated.Subscribe(_ => OnItemRotated()));
        Disposables.Add(_eventController.OnItemPlaced.Subscribe(item => OnItemPlaced(item)));
        Disposables.Add(_eventController.OnItemTaken.Subscribe(_ => OnItemTaken()));

        TaskController.SetTask(0);

        var placedItem = ItemPlacing.PlacedItems[1];
        Vector2 itemScreenPosition = MainCamera.Camera.WorldToScreenPoint(placedItem.transform.position);

        var cursorTween = Object.Instantiate(Dependencies.CicleScaleCursorPrefab, UI.Canvas);
        cursorTween.transform.position = itemScreenPosition;
        _cursor = cursorTween.gameObject;

        var prompt = Object.Instantiate(Dependencies.LeftPromptPrefab, UI.Canvas);
        prompt.text = Localization.GetString("click_to_rotate_item");
        _prompt = prompt.gameObject;
        _prompt.transform.position = itemScreenPosition;

        RaycastBlock.Concentrate(placedItem.ClickImage);
    }

    private void OnItemTaken()
    {
        _cursor.SetActive(false);
        _prompt.SetActive(false);
    }

    private void OnItemPlaced(Item item)
    {
        Vector2 itemScreenPosition = MainCamera.Camera.WorldToScreenPoint(item.transform.position);

        _cursor.SetActive(true);
        _cursor.transform.position = itemScreenPosition;

        _prompt.SetActive(true);
        _prompt.transform.position = itemScreenPosition;
    }

    private void OnItemRotated() => StepCompleted();

}
