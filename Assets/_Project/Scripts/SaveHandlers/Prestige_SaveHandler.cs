namespace VG
{

    public partial class Saves
    {

        public static int GetPrestige(int roomIndex)
        {
            int result = 0;

            foreach (var item in GetItems(roomIndex))
                result += TotalRules.GetItemPrestige(item.rarityType);

            return result;
        }



    }

}


