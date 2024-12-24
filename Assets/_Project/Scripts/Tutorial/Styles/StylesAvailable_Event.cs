using UnityEngine;
using VG2;

public class StylesAvailable_Event : MonoBehaviour
{
    [SerializeField] private GameObject _styleButton;
    [SerializeField] private StyleTutorial _tutorial;
    [SerializeField] private int _fromLevel;

    private void OnEnable()
    {
        Events.onNewLevelReached += OnNewLevelReached;
        TriggerEvent();
    }

    private void OnDisable()
    {
        Events.onNewLevelReached -= OnNewLevelReached;
    }

    private void OnNewLevelReached() => TriggerEvent();

    private void TriggerEvent()
    {
        bool stylesAvailable = PrestigeCalculator.GetCurrentRoomLevel() >= _fromLevel;
        _styleButton.SetActive(stylesAvailable);

        if (stylesAvailable && GameState.styleTutorialCompleted == false)
            _tutorial.Run();
    }


}
