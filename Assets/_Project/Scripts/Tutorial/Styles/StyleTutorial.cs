using UnityEngine;
using VG2;

public class StyleTutorial : MonoBehaviour
{
    [SerializeField] private GameObject _gameplayArrow;
    [SerializeField] private ButtonHandler _openStylesButton;

    
    public void Run()
    {
        _gameplayArrow.SetActive(true);
        _openStylesButton.onCompleted += OnOpenStylesButtonClicked;
    }

    private void OnOpenStylesButtonClicked()
    {
        _openStylesButton.onCompleted -= OnOpenStylesButtonClicked;

        _gameplayArrow.SetActive(false);

        var tutorialModification = UI.Canvas.Find(StylesWindow.Name)
            .GetComponent<StyleWindowTutorialModification>();

        tutorialModification.ShowPrompt();
    }



}
