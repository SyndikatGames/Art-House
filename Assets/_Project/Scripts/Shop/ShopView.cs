using UnityEngine;

public class ShopView : MonoBehaviour
{
    [SerializeField] private NoAdsProductView _noAdsProductView;
    [SerializeField] private BoxProductView _commonBoxView;
    [SerializeField] private BoxProductView _rareBoxView;
    [SerializeField] private BoxProductView _epicBoxView;
    [SerializeField] private BoxProductView _fantasticBoxView;



    /*
    public void Display(ShopModel model)
    {
        _noAdsProductView.Display(model.noAdsPurchased);

        _commonBoxView.Display(model.boxProductModels
            .Find(prod => prod.boxType == BoxType.Common));

        _rareBoxView.Display(model.boxProductModels
            .Find(prod => prod.boxType == BoxType.Rare));

        _epicBoxView.Display(model.boxProductModels
            .Find(prod => prod.boxType == BoxType.Epic));

        _fantasticBoxView.Display(model.boxProductModels
            .Find(prod => prod.boxType == BoxType.Fantastic));

    }
    */


}
