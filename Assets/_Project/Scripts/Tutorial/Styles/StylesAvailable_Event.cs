using UnityEngine;
using VG;

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
        bool stylesAvailable = Configs.GetRoom(0).CurrentLevel >= _fromLevel;
        _styleButton.SetActive(stylesAvailable);

        if (stylesAvailable && Saves.Bool[Key_Save.style_tutorial_completed].Value == false)
            _tutorial.Run();
    }


}
