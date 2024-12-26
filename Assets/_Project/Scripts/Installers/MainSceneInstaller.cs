using Zenject;

public class MainSceneInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<UnboxingController>().FromNew().AsSingle();
        Container.Bind<ShopController>().FromNew().AsSingle();


    }

}
