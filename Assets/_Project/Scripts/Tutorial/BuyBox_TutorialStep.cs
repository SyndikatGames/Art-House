using UnityEngine;
using UnityEngine.UI;

public class BuyBox_TutorialStep : TutorialStep
{
    [SerializeField] private GameObject _cursor;
    [SerializeField] private Button _buyButton;


    public override void RestoreContext()
    {
        _cursor.SetActive(false);
        _buyButton.onClick.RemoveListener(OnBoxBuy);
    }

    public override void Run()
    {
        _cursor.SetActive(true);
        _buyButton.onClick.AddListener(OnBoxBuy);
    }


    private void OnBoxBuy() => StepCompleted();



}
