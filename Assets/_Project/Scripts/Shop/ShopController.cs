using VG2;

public class ShopController
{
    private ShopConfig _shopConfig;
    private UnboxingConfig _unboxingConfig;
    private DesignConfig _designConfig;
    

    public ShopController(ShopConfig shopConfig, UnboxingConfig unboxingConfig, DesignConfig designConfig)
    {
        _shopConfig = shopConfig;
        _unboxingConfig = unboxingConfig;
        _designConfig = designConfig;
    }

    public void OpenShop()
    {
        DiContainer.Create(_shopConfig.ShopViewPrefab, UI.Canvas);
    }

    public void OpenBoxWindow(BoxType boxType)
    {
        DiContainer.Create(_shopConfig.BoxWindowViewPrefab, UI.Canvas);// Display(_designConfig, _unboxingConfig, boxType);
    }

    public bool TryBuyBox(BoxType boxType, int amount)
    {
        

        return false;
    }




}
