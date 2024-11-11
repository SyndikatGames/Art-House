namespace VG
{

    public partial class Saves
    {

        public static int GetPrestige(int roomIndex)
        {
            var itemDataList = ItemList.PlacedItems;
            print("Begin");

            int result = 0;
            for (int i = 0; i < itemDataList.Count; i++)
            {
                if (itemDataList[i].ItemType == ItemType.Room) 
                    continue;

                result += TotalRules.GetPrestige(itemDataList[i].RarityType);
                print($"add {TotalRules.GetPrestige(itemDataList[i].RarityType)}. Res: {result}");
            }
                

            return result;
        }



    }

}


