using VG2;
using Zenject;

namespace PrizeClaw
{
    public class PrizeClawSceneInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            new SceneContainer(Container);

            Container.BindInterfacesAndSelfTo<GameLogicController>().FromNew().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().FromNew().AsSingle();


        }

    }
}


