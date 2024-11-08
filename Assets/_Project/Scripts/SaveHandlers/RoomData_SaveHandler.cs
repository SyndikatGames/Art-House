using System.Collections.Generic;
using UnityEngine;


public struct ItemData
{
    public ItemType itemType;
    public RarityType rarityType;
    public Vector3Int position;
    public List<ItemData> childItems;

    public ItemData(Item item)
    {
        itemType = item.ItemType;
        rarityType = item.RarityType;
        position = item.Position;

        if (item.ChildItems.Count > 0)
        {
            childItems = new List<ItemData>();
            for (int i = 0; i < item.ChildItems.Count; i++)
                childItems.Add(new ItemData(item.ChildItems[i]));
        }
        else childItems = null;
    }

    public ItemData(string data)
    {
        int splitIndex = data.IndexOf('(');

        string mainData = data.Substring(0, splitIndex);
        string childData = data.Substring(splitIndex + 1);

        string[] mainSplitData = mainData.Split('_');
        itemType = (ItemType)int.Parse(mainSplitData[0]);
        rarityType = (RarityType)int.Parse(mainSplitData[1]);
        position = new Vector3Int(
            x: int.Parse(mainSplitData[2]),
            y: int.Parse(mainSplitData[3]),
            z: int.Parse(mainSplitData[4]));

        if (childData.Length > 0)
        {
            childItems = new List<ItemData>();
            string[] splitChildData = childData.Split(')');

            for (int i = 0; i < splitChildData.Length - 1; i++)
                childItems.Add(new ItemData(splitChildData[i]));
        }
        else childItems = null;
    }

    public string ToDataString()
    {
        string data = $"{(int)itemType}_{(int)rarityType}_" +
            $"{position.x}_{position.y}_{position.z}(";

        if (childItems != null)
            foreach (ItemData childItem in childItems)
                data += childItem.ToDataString();

        data += ')';
        return data;
    }

}


namespace VG
{
    public partial class Saves
    {
        public const int roomsAmount = 10;

        public static void SetRoomItems(int roomIndex, List<Item> items)
        {
            string data = string.Empty;

            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.ItemType != ItemType.Room && item.ParentItem.ItemType == ItemType.Room)
                    data += new ItemData(items[i]).ToDataString();
            }
                
            String[Key_Save.room_data(roomIndex)].Value = data;
        }



        public static List<ItemData> GetRoomItems(int roomIndex)
        {
            var items = new List<ItemData>();
            if (String[Key_Save.room_data(roomIndex)].Value == string.Empty) 
                return items;

            string[] splitData = String[Key_Save.room_data(roomIndex)].Value.Split(')');

            for (int i = 0; i < splitData.Length - 1; i++)
                items.Add(new ItemData(splitData[i]));

            return items;
        }

    }

}

