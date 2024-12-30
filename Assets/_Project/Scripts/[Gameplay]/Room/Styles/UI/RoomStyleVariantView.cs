using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;


public class RoomStyleVariantView : MonoBehaviour
{
    [SerializeField] private Image _toRightWallImage1;
    [SerializeField] private Image _toRightWallImage2;
    [SerializeField] private Image _toLeftWallImage1;
    [SerializeField] private Image _toLeftWallImage2;
    [SerializeField] private Image _floorImage;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Button _selectButton;
    [SerializeField] private GameObject _selectedMark;
    [SerializeField] private TextMeshProUGUI _unlockLevelText;
    [SerializeField] private GameObject _newMark;


    public int Index { get; private set; }
    private readonly Color fadeColor = new Color(0.1f, 0.1f, 0.1f, 1f);


    public void UpdateStyleIndex(int index)
    {
        Index = index;

        var styleData = ConfigHub.RoomStyles.GetStyle(index);
        bool styleAvailable = PrestigeCalculator.GetCurrentRoomLevel() > index;
        bool styleSelected = GameState.CurrentRoom.currentStyleIndex.Value == index;

        bool isNew = GameState.CurrentRoom.newStyleIndices.Exists((i) => i == index);
        _newMark.SetActive(isNew);

        SetPreviewSprites(styleData);
        SetPreviewColor(styleAvailable, styleSelected);
        EnableObjects(styleAvailable, styleSelected);
    }

    private void SetPreviewSprites(RoomStyleData styleData)
    {
        _toLeftWallImage1.sprite = styleData.toLeftWallSprite;
        _toLeftWallImage2.sprite = styleData.toLeftWallSprite;
        _toRightWallImage1.sprite = styleData.toRightWallSprite;
        _toRightWallImage2.sprite = styleData.toRightWallSprite;
        _floorImage.sprite = styleData.floorSprite;
    }


    private void SetPreviewColor(bool styleAvailable, bool styleSelected)
    {
        Color previewColor = styleAvailable ? Color.white : fadeColor;
        if (styleSelected) previewColor = Color.white;

        _toLeftWallImage1.color = previewColor;
        _toLeftWallImage2.color = previewColor;
        _toRightWallImage1.color = previewColor;
        _toRightWallImage2.color = previewColor;
        _floorImage.color = previewColor;
    }

    private void EnableObjects(bool styleAvailable, bool styleSelected)
    {
        if (styleSelected)
        {
            _selectedMark.SetActive(true);
            _lock.SetActive(false);
            _unlockLevelText.gameObject.SetActive(false);
            _selectButton.interactable = false;
        }
        else
        {
            _selectButton.interactable = styleAvailable;
            _lock.SetActive(!styleAvailable);
            _unlockLevelText.gameObject.SetActive(!styleAvailable);
            _unlockLevelText.text = $"{Localization.GetString("level")} {Index}";
            _selectedMark.SetActive(false);
        }
    }


}
