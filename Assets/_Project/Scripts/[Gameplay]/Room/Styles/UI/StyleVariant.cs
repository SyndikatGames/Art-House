using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VG2;


public class StyleVariant : MonoBehaviour
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




    private int _index;
    private readonly Color fadeColor = new Color(0.1f, 0.1f, 0.1f, 1f);


    public void UpdateStyleIndex(int index)
    {
        _index = index;

        var styleData = Configs.GetStyles(0).GetStyle(index);
        bool styleAvailable = PrestigeCalculator.GetCurrentRoomLevel() >= index;
        bool styleSelected = GameState.CurrentRoom.currentStyleIndex.Value == index;

        SetPreviewSprites(styleData);
        SetPreviewColor(styleAvailable, styleSelected);
        EnableObjects(styleAvailable, styleSelected);

        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(OnSelectButtonPressed);
    }

    private void SetPreviewSprites(StyleData styleData)
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
            _unlockLevelText.text = $"{Localization.GetString("level")} {_index}";
            _selectedMark.SetActive(false);
        }

        //_newMark.SetActive(Saves.StyleIsNew(_index) && styleAvailable);
    }


    private void OnSelectButtonPressed()
    {
        /*
        _gameState.RoomState.currentStyleIndex.Value = _index;

        Saves.Int[Key_Save.current_style_index(0)].Value = _index;
        Saves.SetStyleNew(_index, false);
        */
    }


}
