using R3;
using TMPro;
using UnityEngine;


public class PrestigeHighlightTutorialStep : TutorialStep
{
    private EventController _eventController;
    private GameObject _prompt;


    public PrestigeHighlightTutorialStep(EventController eventController)
    {
        _eventController = eventController;
    }

    public override void RestoreContext()
    {
        base.RestoreContext();
        if (_prompt != null) Object.Destroy(_prompt);
    }

    public override void Run()
    {
        Disposables.Add(_eventController.OnItemPlacedFromCard.Subscribe(_ => OnItemPlacedFromCard()));

        TaskController.SetTask(0);

        _prompt = Object.Instantiate(Dependencies.ArrowPromptPrefab, UI.Canvas);
        _prompt.transform.position = Dependencies.LevelRect.position;
        _prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Каждый предмет увеличивает уровень престижа комнаты";

    }


    private void OnItemPlacedFromCard()
    {
        StepCompleted();
    }


}
