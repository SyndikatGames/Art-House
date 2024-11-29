using UnityEngine;
using UnityEngine.UI;
using VG;

public class GoToShop_TutorialStep : TutorialStep
{
    [SerializeField] private GameObject _prompt;
    [SerializeField] private Button _shopButton;


    public override void RestoreContext()
    {
        _prompt.SetActive(false);
        _shopButton.onClick.RemoveListener(OnShopOpened);
    }

    public override void Run()
    {
        _prompt.SetActive(true);
        _shopButton.onClick.AddListener(OnShopOpened);
    }


    private void OnShopOpened() => StepCompleted();


}
