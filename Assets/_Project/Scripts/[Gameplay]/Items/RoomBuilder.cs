using UnityEngine;
using VG2;

public class RoomBuilder : MonoBehaviour
{
    [SerializeField] private Item _rootItem;
    [SerializeField] private RoomExpansion _roomExpansion;


    private void Awake()
    {
        if (Saves.Initialized) BuildRoom();
    }


    private void BuildRoom()
    {
        _rootItem.SetPlace(Vector3Int.zero, placeGrid: null);
        _roomExpansion.UpdateRoomSize();

        var itemHierarchy = GameState.CurrentRoom.placedItemsHierarchy;

        foreach (var itemData in itemHierarchy)
            InstantiateItemRecursive(itemData, parent: _rootItem);
            
        ItemPlacing.ResortOrder();
    }



    private void InstantiateItemRecursive(PlacedItemModel itemData, Item parent)
    {
        var itemInstance = Instantiate(Prefabs.GetItem(itemData.itemType), 
            Vector3.zero, Quaternion.identity);

        var placeGrid = parent.GetPlaceGrid(itemData.position, itemInstance.PlaceType, itemData.side);
        itemInstance.SetPlace(itemData.position, placeGrid);
        itemInstance.SetSide(itemData.side);
        itemInstance.SetRarity(itemData.rarityType);

        if (itemData.childItems != null)
            foreach (var childData in itemData.childItems)
                InstantiateItemRecursive(childData, itemInstance);
                
    }



}
