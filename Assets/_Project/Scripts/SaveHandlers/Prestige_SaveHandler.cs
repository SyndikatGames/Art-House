namespace VG
{

    public static class Prestige_SaveHandler
    {

        public static int GetPrestige(int roomIndex)
        {
            var itemDataList = Saves.GetRoomItems(roomIndex);

            int result = 0;
            for (int i = 0; i < itemDataList.Count; i++)
                result += TotalRules.GetPrestige(itemDataList[i].rarityType);

            return result;
        }



    }

}


