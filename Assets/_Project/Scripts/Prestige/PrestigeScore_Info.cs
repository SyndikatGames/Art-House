using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;

public class PrestigeScore_Info : ReactiveView
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _fillImage;

    private const int roomIndex = 0;


    protected override void Subscribe()
    {
        ItemList.onUpdated += Display;
    }

    protected override void Dispose()
    {
        ItemList.onUpdated -= Display;
    }

    protected override void Display()
    {
        var roomConfig = Configs.GetRoom(roomIndex);
        int prestige = roomConfig.GetCurrentPrestige();
        int require = roomConfig.GetCurrentPrestigeRequire();

        _text.text = $"{prestige}/{require}";
        _fillImage.fillAmount = (float)prestige / require;
    }

}