using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;

public class TimeBoxes_Info : Info
{
    [SerializeField] private TextMeshProUGUI _counterText;
    [SerializeField] private GameObject _counter;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private Image _fillImage;
    [SerializeField] private GameObject _shine;

    private int _roomIndex = 0;

    protected override void Subscribe()
    {
        Saves.Float[Key_Save.random_boxes(_roomIndex)].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.Float[Key_Save.random_boxes(_roomIndex)].onChanged -= UpdateValue;
    }


    
    
    protected override void UpdateValue()
    {
        float timeBoxes = Saves.Float[Key_Save.random_boxes(_roomIndex)].Value;

        float boxFullness = timeBoxes % 1f;
        float secondsLeft = (1f - boxFullness) * 3600f / TotalRules.GetBoxesPerHour(_roomIndex);
        _timerText.text = secondsLeft.ToTimeMinutesString();

        if (timeBoxes < 1f)
        {
            _fillImage.fillAmount = boxFullness;
            _shine.SetActive(false);
            _counter.SetActive(false);
        }
        else
        {
            int fullBoxes = (int)timeBoxes;
            _counterText.text = fullBoxes.ToString();
            _fillImage.fillAmount = 1f;
            _shine.SetActive(true);
            _counter.SetActive(true);
        }


    }
    
}