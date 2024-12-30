using UnityEngine;
using UnityEngine.Tilemaps;
using VG2;
using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    [SerializeField] private Item _rootItem;
    [SerializeField] private Tilemap _roomTilemap;

    public override void InstallBindings()
    {
        new SceneContainer(Container);

        var eventController = new EventController();
        Container.BindInterfacesAndSelfTo<EventController>().FromInstance(eventController).AsSingle();
        Container.Bind<UnboxingController>().FromNew().AsSingle();
        Container.Bind<ShopController>().FromNew().AsSingle();
        

        RoomController roomController = new RoomController(_rootItem, _roomTilemap);
        Container.BindInterfacesAndSelfTo<RoomController>().FromInstance(roomController).AsSingle();

        RoomStyleController roomStyleController = new RoomStyleController(eventController);
        Container.BindInterfacesAndSelfTo<RoomStyleController>().FromInstance(roomStyleController).AsSingle();


    }


}
