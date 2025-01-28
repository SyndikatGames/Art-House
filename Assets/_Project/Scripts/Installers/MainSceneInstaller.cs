using PrizeClaw;
using UnityEngine;
using UnityEngine.Tilemaps;
using VG2;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _camera;
    [SerializeField] private TutorialSceneDependencies _tutorialDependencies;
    [SerializeField] private Item _rootItem;
    [SerializeField] private Tilemap _roomTilemap;

    public override void InstallBindings()
    {
        new SceneContainer(Container);
        var gameTime = new GameTime();
        

        var events = new EventController();
        var camera = new CameraController(_camera);


        Container.BindInterfacesAndSelfTo<EventController>().FromInstance(events).AsSingle();

        var unboxing = new UnboxingController();
        Container.Bind<UnboxingController>().FromInstance(unboxing).AsSingle();

        var shop = new ShopController();
        Container.Bind<ShopController>().FromInstance(shop).AsSingle();

        var prizeClaw = new GameController();
        Container.Bind<GameController>().FromInstance(prizeClaw).AsSingle();

        var roomExpansion = new RoomExpansionController();
        Container.Bind<RoomExpansionController>().FromInstance(roomExpansion).AsSingle();

        var tasks = new TaskController();
        Container.BindInterfacesAndSelfTo<TaskController>().FromInstance(tasks).AsSingle();

        var income = new IncomeController();
        Container.BindInterfacesAndSelfTo<IncomeController>().FromInstance(income).AsSingle();
        gameTime.AddOfflineTimeHandler(income);

        Container.BindInterfacesAndSelfTo<RoomBuilderController>()
            .FromInstance(new RoomBuilderController(_rootItem, _roomTilemap)).AsSingle();

        var roomStyle = new RoomStyleController(events);
        Container.BindInterfacesAndSelfTo<RoomStyleController>().FromInstance(roomStyle).AsSingle();

        var prestige = new PrestigeController(events);
        Container.BindInterfacesAndSelfTo<PrestigeController>().FromInstance(prestige).AsSingle();

        Container.BindInterfacesAndSelfTo<CameraController>()
            .FromInstance(camera).AsSingle();

        Container.BindInterfacesAndSelfTo<TutorialController>()
            .FromInstance(new TutorialController(_tutorialDependencies, 
            prestige, events, camera, tasks, shop, unboxing, prizeClaw, roomExpansion, roomStyle)).AsSingle();

        
    }


}
