using UnityEngine;
using UnityEngine.UI;
using VG;

public class PrestigeScore_Info : Info
{
    [SerializeField] private Image _fillImage;

    private const int roomIndex = 0;


    protected override void Subscribe()
    {
        ItemList.onUpdated += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        ItemList.onUpdated -= UpdateValue;
    }

    protected override void UpdateValue()
    {
        var roomConfig = Configs.GetRoom(roomIndex);
        int prestige = roomConfig.GetCurrentPrestige();
        int require = roomConfig.GetCurrentPrestigeRequire();

        text.text = $"{prestige}/{require}";
        _fillImage.fillAmount = (float)prestige / require;
    }

}