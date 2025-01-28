using UnityEngine;
using UnityEngine.UI;
using VG2;

public class SettingsSystem : MonoBehaviour
{
    [SerializeField] private GameObject _soundCrossout;
    [SerializeField] private GameObject _musicCrossout;

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;


    private void Start()
    {
        Sound.Settings.Apply();

        _soundButton.onClick.AddListener(OnSoundButtonClicked);
        _musicButton.onClick.AddListener(OnMusicButtonClicked);
    }

    private void Update()
    {
        _soundCrossout.SetActive(!Sound.Settings.sfxEnabled);
        _musicCrossout.SetActive(!Sound.Settings.musicEnabled);

    }


    private void OnMusicButtonClicked()
    {
        Sound.Settings.musicEnabled = !Sound.Settings.musicEnabled;
    }

    private void OnSoundButtonClicked()
    {
        Sound.Settings.sfxEnabled = !Sound.Settings.sfxEnabled;
    }


    






}
