using UnityEngine;
using UnityEngine.UI;

public class ExitShop_TutorialStep : TutorialStep
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private Button _shopButton;


    public override void RestoreContext()
    {
        _cursor.SetActive(false);
        _shopButton.onClick.RemoveListener(OnShopExit);
    }

    public override void Run()
    {
        _cursor.SetActive(true);
        _shopButton.onClick.AddListener(OnShopExit);
    }


    private void OnShopExit() => StepCompleted();

}
