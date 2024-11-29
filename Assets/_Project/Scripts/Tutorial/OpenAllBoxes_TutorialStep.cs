using TMPro;
using UnityEngine;

public class OpenAllBoxes_TutorialStep : TutorialStep
{
    [SerializeField] private TextMeshProUGUI _promptText;


    public override void RestoreContext()
    {
        _promptText.gameObject.SetActive(false);
        Events.onNewItemPlaced -= OnNewItemPlaced;
    }

    public override void Run()
    {
        _promptText.gameObject.SetActive(true);
        _promptText.text = "Открой все коробки и улучши комнату!";

        Events.onNewItemPlaced += OnNewItemPlaced;
    }

    private void OnNewItemPlaced() => StepCompleted();

}
