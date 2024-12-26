
public struct CardModel
{
    public ItemType itemType;
    public RarityType rarityType;
    public int amount;


    public string ToDataString() => $"{(int)itemType},{(int)rarityType},{amount}";

    public CardModel(Item item)
    {
        itemType = item.ItemType;
        rarityType = item.RarityType;
        amount = 1;
    }

    public CardModel(string data)
    {
        var splitData = data.Split(',');

        itemType = (ItemType)int.Parse(splitData[0]);
        rarityType = (RarityType)int.Parse(splitData[1]);
        amount = int.Parse(splitData[2]);
    }



}
