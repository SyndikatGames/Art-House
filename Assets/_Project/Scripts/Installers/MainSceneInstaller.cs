using UnityEngine;
using VG2;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private DesignConfig _designConfig;
    [SerializeField] private UnboxingConfig _unboxingConfig;
    [SerializeField] private ShopConfig _shopConfig;


    public override void InstallBindings()
    {
        UnboxingController unboxingController = new UnboxingController(_unboxingConfig);
        Container.Bind<UnboxingController>().FromInstance(unboxingController).AsSingle();

        ShopController shopController = new ShopController(_shopConfig, _unboxingConfig, _designConfig);
        Container.Bind<ShopController>().FromInstance(shopController).AsSingle();


    }

}
