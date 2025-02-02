using R3;
using TMPro;
using UnityEngine;
using VG2;


public class PrestigeHighlightTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _prompt;
    private GameObject _cursor;


    public PrestigeHighlightTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }

    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_prompt != null) Object.Destroy(_prompt);
        if (_cursor != null) Object.Destroy(_cursor);
    }

    public override void Run()
    {
        Disposables.Add(_eventController.OnItemPlacedFromCard.Subscribe(_ => OnItemPlacedFromCard()));
        Disposables.Add(_eventController.OnCardTaken.Subscribe(_ => OnCardTaken()));
        Disposables.Add(_eventController.OnCardReleased.Subscribe(_ => OnCardReleased()));

        TaskController.SetTask(0);

        _prompt = Object.Instantiate(Dependencies.ArrowPromptPrefab, UI.Canvas);
        _prompt.transform.position = Dependencies.LevelRect.position;
        _prompt.GetComponentInChildren<TextMeshProUGUI>().text = Localization.GetString("prestige_tutorial_prompt");

        var cursorTween = Object.Instantiate(Dependencies.PressAndFadeMoveCursorPrefab, UI.Canvas);

        Vector2 from = (Vector2)Dependencies.InventoryRect.position + Vector2.up * 50f;
        Vector2 to = ScreenCalculator.GetScreenCenter();

        cursorTween.Run(from, to);

        _cursor = cursorTween.gameObject;



    }

    private void OnCardReleased()
    {
        _cursor.SetActive(true);
    }

    private void OnCardTaken()
    {
        _cursor.SetActive(false);
    }




    private void OnItemPlacedFromCard()
    {
        StepCompleted();
    }


}
