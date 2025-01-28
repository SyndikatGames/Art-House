using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;

public class LevelUpRoomWindowView : MonoBehaviour
{
    [SerializeField] private List<Image> _starImages;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _upgradeIncomeText;


    [Header("Resources:")]
    [SerializeField] private Sprite _emptyStarSprite;
    [SerializeField] private Sprite _starSprite;
    [SerializeField] private Sprite _superStarSprite;


    private const int STARS_AMOUNT = 5;



    private void OnEnable() => Display();


    private void Display()
    {
        int newRoomLevel = PrestigeCalculator.GetCurrentRoomLevel();
        int previousRoomLevel = newRoomLevel - 1;

        for (int i = 0; i < STARS_AMOUNT; i++)
        {
            int starNumber = i + 1;

            Sprite starSprite = _emptyStarSprite;

            if (newRoomLevel >= STARS_AMOUNT + starNumber)
                starSprite = _superStarSprite;

            else if (newRoomLevel >= starNumber) starSprite = _starSprite;

            _starImages[i].sprite = starSprite;
        }

        _levelText.text = $"{newRoomLevel} {Localization.GetString("level")}";


        float previousIncome = ConfigHub.Income.GetIncomePerHour(previousRoomLevel);
        float newIncome = ConfigHub.Income.GetIncomePerHour(newRoomLevel);

        _upgradeIncomeText.text = $"<sprite=0>{previousIncome.ToShortNumber()} <color=green>> " +
            $"{newIncome.ToShortNumber()}\n</color><size=46>{Localization.GetString("per_hour")}";





    }



}
