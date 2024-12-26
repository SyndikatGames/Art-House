using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;
using R3;

public class RoomPrestigePointsView : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _prestigePointsText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private Image _progressFillImage;

    protected override void Subscribe()
    {
        disposables.Add(GameState.CurrentRoom.placedItemsHierarchy.onChanged.Subscribe(_ => Display()));
    }

    protected override void Display()
    {
        float currentPoints = PrestigeCalculator.GetCurrentRoomPrestigePoints();
        float requiredPoints = PrestigeCalculator.GetCurrentRoomPrestigeRequire();
        int roomLevel = PrestigeCalculator.GetCurrentRoomLevel();

        _levelText.text = roomLevel.ToString();
        _prestigePointsText.text = $"{currentPoints.ToShortNumber()} / {requiredPoints.ToShortNumber()}";

        _progressFillImage.fillAmount = currentPoints / requiredPoints;
    }

    




}
