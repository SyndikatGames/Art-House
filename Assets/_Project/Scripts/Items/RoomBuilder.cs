using UnityEngine;
using VG;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Item _rootItem;


    private void Awake() => BuildRoom();

    private void BuildRoom()
    {
        _rootItem.SetPlace(Vector3Int.zero, parent: null);

        var roomItemsDataList = Saves.GetRoomItems(roomIndex: 0);

        foreach (var roomItem in roomItemsDataList)
        {

        }

    }

    private Item InstantiateItem(ItemData itemData)
    {
        foreach (var childData in itemData.childItems)
        {
            var instChild = InstantiateItem(childData);
        }

        /*
        var itemInstance = Instantiate(Configs.GetItem(itemData.itemType)
            .ItemPrefab, position, Quaternion.identity);

        itemInstance.SetPlace(itemData.position, parent: null);
        itemInstance.
        */
        return null;
    }



}
