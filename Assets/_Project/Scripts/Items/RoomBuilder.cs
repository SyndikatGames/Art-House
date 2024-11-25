using UnityEngine;
using VG;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Item _rootItem;


    private void Awake()
    {
        if (Saves.Initialized) BuildRoom();
    }

    private void BuildRoom()
    {
        print($"Build: {Saves.String[Key_Save.room_data(0)].Value}");
        _rootItem.SetPlace(Vector3Int.zero, placeGrid: null);

        var roomItemsDataList = Saves.GetRoomItemAcrhitecture(roomIndex: 0);

        foreach (var itemData in roomItemsDataList)
        {
            Debug.Log($"Inst: {itemData.itemType} on Room");
            InstantiateItem(itemData, parent: _rootItem);
        }
            

        ItemList.ResortOrder();
        ItemList.UpdateItems();
    }

    private void InstantiateItem(ItemData itemData, Item parent)
    {
        print($"Instantiate: {itemData.itemType} on {parent.name}");
        var itemInstance = Instantiate(Prefabs.GetItem(itemData.itemType), 
            Vector3.zero, Quaternion.identity);

        var placeGrid = parent.GetPlaceGrid(itemData.position, itemInstance.PlaceType, itemData.side);
        itemInstance.SetPlace(itemData.position, placeGrid);
        itemInstance.SetSide(itemData.side);
        itemInstance.SetRarity(itemData.rarityType);

        if (itemData.childItems != null)
            foreach (var childData in itemData.childItems)
            {
                Debug.Log(itemData.childItems.Count);
                InstantiateItem(childData, itemInstance);
            }
                
    }



}
