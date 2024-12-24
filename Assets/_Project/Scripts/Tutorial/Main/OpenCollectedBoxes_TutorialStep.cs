using TMPro;
using UnityEngine;
using VG2;

public class OpenCollectedBoxes_TutorialStep : TutorialStep
{
    [SerializeField] private TextMeshProUGUI _prompText;


    public override void RestoreContext()
    {
        _prompText.gameObject.SetActive(false);
        Events.onNewItemPlaced -= OnNewItemPlaced;
        Events.onNewItemDestroyed -= OnNewItemDestroyed;
    }

    public override void Run()
    {
        _prompText.gameObject.SetActive(true);
        _prompText.text = Localization.GetString("tutorial_open_new_boxes");
        Events.onNewItemPlaced += OnNewItemPlaced;
        Events.onNewItemDestroyed += OnNewItemDestroyed;
    }

    private void OnNewItemDestroyed() => StepCompleted();
    private void OnNewItemPlaced() => StepCompleted();

}
