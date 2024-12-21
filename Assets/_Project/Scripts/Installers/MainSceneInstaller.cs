using UnityEngine;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private UnboxingConfig _boxesConfig;


    public override void InstallBindings()
    {

        UnboxingController unboxingController = new UnboxingController(_boxesConfig);
        Container.Bind<UnboxingController>().FromInstance(unboxingController).AsSingle();

        Debug.Log("Bind");
    }

}
