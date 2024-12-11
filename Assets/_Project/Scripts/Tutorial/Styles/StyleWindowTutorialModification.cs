using UnityEngine;
using UnityEngine.UI;
using VG;

public class StyleWindowTutorialModification : MonoBehaviour
{
    [SerializeField] private GameObject _tutorialPrompt;
    [SerializeField] private Button _tutorialButton;

    public void ShowPrompt()
    {
        _tutorialPrompt.SetActive(true);
        _tutorialButton.onClick.AddListener(OnTutorialButtonClicked);
    }

    private void OnTutorialButtonClicked()
    {
        _tutorialButton.onClick.RemoveListener(OnTutorialButtonClicked);
        _tutorialPrompt.SetActive(false);
        Saves.Bool[Key_Save.style_tutorial_completed].Value = true;
    }


}
