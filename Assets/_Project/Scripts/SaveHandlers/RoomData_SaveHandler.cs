using System.Collections.Generic;
using UnityEngine;



namespace VG
{
    public partial class Saves
    {
        public const int roomsAmount = 1;

        private static List<List<PlacedItemModel>> _itemArchitectures;
        private static List<List<PlacedItemModel>> _itemsLists;

        public static void SetRoomItems(int roomIndex, List<Item> items)
        {
            string data = string.Empty;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.ItemType != ItemType.Room && item.ParentItem.ItemType == ItemType.Room)
                    data += new PlacedItemModel(items[i]).ToDataString();
            }

            GenerateItems(roomIndex, data);
            String[Key_Save.room_data(roomIndex)].Value = data;
        }

        private static void GenerateItems(int roomIndex, string data)
        {
            if (_itemArchitectures == null)
            {
                _itemArchitectures = new List<List<PlacedItemModel>>(roomsAmount);
                _itemsLists = new List<List<PlacedItemModel>>(roomsAmount);
                for (int i = 0; i < roomsAmount; i++)
                {
                    _itemArchitectures.Add(null);
                    _itemsLists.Add(null);
                }
            }

            var itemArchitecture = _itemArchitectures[roomIndex] = new List<PlacedItemModel>();
            if (data != string.Empty)
            {
                var splitData = PlacedItemModel.SplitByItemData(data);

                for (int i = 0; i < splitData.Count; i++)
                    itemArchitecture.Add(new PlacedItemModel(splitData[i]));
            }

            var itemList = _itemsLists[roomIndex] = new List<PlacedItemModel>();
            AddItemsToList(itemList, itemArchitecture);
        }

        private static void AddItemsToList(List<PlacedItemModel> listForAdd, List<PlacedItemModel> items)
        {
            foreach (PlacedItemModel item in items)
            {
                listForAdd.Add(item);

                if (item.childItems != null)
                    AddItemsToList(listForAdd, item.childItems);
            }
        }

        public static List<PlacedItemModel> GetRoomItemAcrhitecture(int roomIndex)
        {
            if (_itemArchitectures == null || _itemArchitectures[roomIndex] == null)
            {
                var data = String[Key_Save.room_data(roomIndex)].Value;
                GenerateItems(roomIndex, data);
            }

            return _itemArchitectures[roomIndex];
        }

        public static List<PlacedItemModel> GetItems(int roomIndex)
        {
            if (_itemsLists == null || _itemsLists[roomIndex] == null)
            {
                var data = String[Key_Save.room_data(roomIndex)].Value;
                GenerateItems(roomIndex, data);
            }

            return _itemsLists[roomIndex];
        }


    }

}

