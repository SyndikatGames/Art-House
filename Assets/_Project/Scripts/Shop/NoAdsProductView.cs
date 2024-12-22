using UnityEngine;

public class NoAdsProductView : MonoBehaviour
{
    [SerializeField] private GameObject _purchaseButton;
    [SerializeField] private GameObject _purchasedLabel;
    
    public void Display(bool noAdsPurchased)
    {
        _purchaseButton.SetActive(!noAdsPurchased);
        _purchasedLabel.SetActive(noAdsPurchased);
    }

}
