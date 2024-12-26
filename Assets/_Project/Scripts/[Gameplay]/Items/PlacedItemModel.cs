using System.Collections.Generic;
using UnityEngine;

public struct PlacedItemModel
{
    public ItemType itemType;
    public RarityType rarityType;
    public Vector3Int position;
    public Side side;
    public List<PlacedItemModel> childItems;

    public PlacedItemModel(Item item)
    {
        itemType = item.ItemType;
        rarityType = item.RarityType;
        position = item.Position;
        side = item.CurrentSide;

        if (item.ChildItems.Count > 0)
        {
            childItems = new List<PlacedItemModel>();
            for (int i = 0; i < item.ChildItems.Count; i++)
                childItems.Add(new PlacedItemModel(item.ChildItems[i]));
        }
        else childItems = null;
    }

    public PlacedItemModel(string data)
    {
        FindBracketPair(data, out int openIndex, out int closeIndex);

        string mainData = data.Substring(0, openIndex);
        string childData = data.Substring(openIndex + 1, closeIndex - openIndex - 1);

        string[] mainSplitData = mainData.Split('_');

        itemType = (ItemType)int.Parse(mainSplitData[0]);
        rarityType = (RarityType)int.Parse(mainSplitData[1]);

        position = new Vector3Int(
            x: int.Parse(mainSplitData[2]),
            y: int.Parse(mainSplitData[3]),
            z: int.Parse(mainSplitData[4]));

        side = (Side)int.Parse(mainSplitData[5]);

        if (childData.Length > 0)
        {
            childItems = new List<PlacedItemModel>();
            var splitChildData = SplitByItemData(childData);

            for (int i = 0; i < splitChildData.Count; i++)
                childItems.Add(new PlacedItemModel(splitChildData[i]));
        }
        else childItems = null;
    }

    private static void FindBracketPair(string data, out int openIndex, out int closeIndex)
    {
        openIndex = data.IndexOf('(');
        closeIndex = 0;

        for (int i = openIndex + 1, depth = 1; i < data.Length; i++)
        {
            if (data[i] == '(') depth++;
            if (data[i] == ')') depth--;

            if (depth == 0)
            {
                closeIndex = i;
                return;
            }
        }
    }

    public static List<string> SplitByItemData(string data)
    {
        List<string> result = new List<string>();
        var noHandledData = new string(data);

        while (noHandledData.Length > 0)
        {
            FindBracketPair(noHandledData, out int openIndex, out int closeIndex);
            string itemData = noHandledData.Substring(0, closeIndex + 1);
            result.Add(itemData);

            noHandledData = noHandledData.Substring
                (closeIndex + 1, noHandledData.Length - itemData.Length);
        }

        return result;
    }



    public string ToDataString()
    {
        string data = $"{(int)itemType}_{(int)rarityType}_" +
            $"{position.x}_{position.y}_{position.z}_{(int)side}(";

        if (childItems != null)
            foreach (PlacedItemModel childItem in childItems)
                data += childItem.ToDataString();

        data += ')';
        return data;
    }
}
