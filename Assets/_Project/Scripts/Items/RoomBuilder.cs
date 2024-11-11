using UnityEngine;
using VG;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Item _rootItem;


    private void Start() => BuildRoom();

    private void BuildRoom()
    {
        _rootItem.SetPlace(Vector3Int.zero, parent: null);

        var roomItemsDataList = Saves.GetRoomItemAcrhitecture(roomIndex: 0);

        foreach (var itemData in roomItemsDataList)
            InstantiateItem(itemData, parent: null);

        ItemList.ResortOrder();
        ItemList.UpdateItems();
    }

    private void InstantiateItem(ItemData itemData, Item parent)
    {
        var itemInstance = Instantiate(Configs.GetItem(itemData.itemType)
            .ItemPrefab, Vector3.zero, Quaternion.identity);

        itemInstance.SetPlace(itemData.position, parent);
        itemInstance.SetSide(itemData.side);
        itemInstance.SetRarity(itemData.rarityType);

        if (itemData.childItems != null)
            foreach (var childData in itemData.childItems)
                InstantiateItem(childData, itemInstance);
    }



}
