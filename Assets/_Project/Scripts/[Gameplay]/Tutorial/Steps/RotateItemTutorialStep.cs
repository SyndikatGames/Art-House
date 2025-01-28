using R3;
using TMPro;
using UnityEngine;

public class RotateItemTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _cursor;
    private GameObject _prompt;
    private GameObject _canNotRotatePrompt;
    private TextMeshProUGUI _canNotRotatePromptPrefab;


    public RotateItemTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }


    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_cursor != null) Object.Destroy(_cursor);
        if (_prompt != null) Object.Destroy(_prompt);
        if (_canNotRotatePrompt != null) Object.Destroy(_canNotRotatePrompt);
    }


    public override void Run()
    {
        _canNotRotatePromptPrefab = Dependencies.RedPromptPrefab;

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
        prompt.text = "Кликни на предмет,\nчтобы повернуть его.";
        _prompt = prompt.gameObject;
        _prompt.transform.position = itemScreenPosition;

        

        if (!placedItem.RotateAvailable)
            ShowCanNotRotatePrompt(itemScreenPosition);

        
    }

    private void OnItemTaken()
    {
        _cursor.SetActive(false);
        _prompt.SetActive(false);

        if (_canNotRotatePrompt != null)
            _canNotRotatePrompt.SetActive(false);
    }

    private void OnItemPlaced(Item item)
    {
        Vector2 itemScreenPosition = MainCamera.Camera.WorldToScreenPoint(item.transform.position);

        if (!item.RotateAvailable)
            ShowCanNotRotatePrompt(itemScreenPosition);

        _cursor.SetActive(true);
        _cursor.transform.position = itemScreenPosition;

        _prompt.SetActive(true);
        _prompt.transform.position = itemScreenPosition;
    }

    private void OnItemRotated() => StepCompleted();


    private void ShowCanNotRotatePrompt(Vector2 position)
    {
        if (_canNotRotatePrompt == null)
        {
            var instance = Object.Instantiate(_canNotRotatePromptPrefab, UI.Canvas);
            instance.text = "Не хватает места.\nПеремести предмет подальше от края комнаты";
            _canNotRotatePrompt = instance.gameObject;
        }

        _canNotRotatePrompt.SetActive(true);
        _canNotRotatePrompt.transform.position = position;
    }

}
