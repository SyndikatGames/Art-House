using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG;

public class RoomWindow : MonoBehaviour
{
    [System.Serializable]
    private struct BonusText
    {
        public BonusType bonusType;
        public TextMeshProUGUI text;
    }

    
    [SerializeField] private GameObject _infoSection;
    [SerializeField] private GameObject _newLevelSection;
    [SerializeField] private TextMeshProUGUI _levelText;

    [SerializeField] private Sprite _emptyStarSprite;
    [SerializeField] private Sprite _starSprite;
    [SerializeField] private Sprite _superStarSprite;

    [SerializeField] private List<Image> _starImages;
    [SerializeField] private List<BonusText> _bonusTexts;


    public void OpenInfo()
    {
        _infoSection.SetActive(true);
        _newLevelSection.SetActive(false);
        UpdateValues();
    }

    public void OpenNewLevel()
    {
        _infoSection.SetActive(false);
        _newLevelSection.SetActive(true);
        UpdateValues();
    }

    private void UpdateValues()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        var roomConfig = Configs.GetRoom(roomIndex);

        int level = roomConfig.CurrentLevel;
        Sprite mainStarSprite = level <= 5 ? _starSprite : _superStarSprite;
        Sprite backStarSprite = level <= 5 ? _emptyStarSprite : _starSprite;

        int stars = level % 6;
        for (int i = 0; i < stars; i++)
            _starImages[i].sprite = mainStarSprite;
        for (int i = stars; i < 5; i++)
            _starImages[i].sprite = backStarSprite;

        _levelText.text = $"Бонусы {level}-го уровня:";


        var bonuses = roomConfig.CurrentBonuses;
        foreach (var bonusText in _bonusTexts)
        {
            if (bonuses.ContainsKey(bonusText.bonusType))
            {
                var bonusValue = bonuses[bonusText.bonusType];
                bonusText.text.gameObject.SetActive(true);
                bonusText.text.text = BonusDescription.Get(bonusText.bonusType, bonusValue);
            }
            else bonusText.text.gameObject.SetActive(false);
        }

    }


}

public static partial class Prefabs
{
    public static RoomWindow RoomWindow =>
        Resources.Load<RoomWindow>("Windows/Room");
}
