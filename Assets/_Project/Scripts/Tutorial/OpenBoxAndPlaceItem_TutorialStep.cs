using TMPro;
using UnityEngine;
using VG;

public class OpenBoxAndPlaceItem_TutorialStep : TutorialStep
{
    [SerializeField] private TextMeshProUGUI _promptText;
    [SerializeField] private GameObject _openBoxPrompt;
    [SerializeField] private GameObject _placeItemPrompt;
    [SerializeField] private CursorMove_Tween _cursorTween;
    [SerializeField] private Transform _moveCursorEndPoint;

    public override void RestoreContext()
    {
        _promptText.gameObject.SetActive(false);
        _openBoxPrompt.SetActive(false);
        _placeItemPrompt.SetActive(false);
        Events.onBoxOpened -= OnBoxOpened;
        Events.onItemPlaced -= OnItemPlaced;
    }

    public override void Run()
    {
        _promptText.gameObject.SetActive(true);

        if (DeviceInfo.DeviceType == VG.DeviceType.Desktop)
            _promptText.text = Localization.GetString("tutorial_move_desctop");

        else _promptText.text = Localization.GetString("tutorial_move_mobile");


        if (Saves.GetNormalBoxesAmount() == 0)
            Saves.AddBoxes(RarityType.Common, TutorialBoxOpening.boxesAmount);

        _openBoxPrompt.SetActive(true);

        Events.onBoxOpened += OnBoxOpened;
    }

    private void OnBoxOpened(Item item)
    {
        Events.onBoxOpened -= OnBoxOpened;

        _openBoxPrompt.SetActive(false);
        _placeItemPrompt.SetActive(true);

        Vector2 fromPosition = (Vector2)item.transform.position + Vector2.up * 0.4f;
        _cursorTween.Run(fromPosition, _moveCursorEndPoint.position);

        Events.onItemPlaced += OnItemPlaced;
    }

    private void OnItemPlaced() => StepCompleted();

}
