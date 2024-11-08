using System;
using static VG.Saves;


namespace VG
{
    public static class SaveCreator
    {


        public static void Create(StartSaveValues startValues)
        {
            new ItemInt(Key_Save.test_count, 0);
            new ItemBool(Key_Save.ads_enabled, true);
            new ItemInt(Key_Save.gems, 0);


            foreach (var rarityType in EnumData.GetRarityTypes())
            {
                int amount = 0;
                if (rarityType == RarityType.Common) amount = 20;

                new ItemInt(Key_Save.boxes_amount(rarityType), amount);
            }

            for (int i = 0; i < roomsAmount; i++)
                new ItemString(Key_Save.room_data(i), string.Empty);


            new ItemString(Key_Save.time.box_accumulation, DateTime.Now.ToString());
            new ItemFloat(Key_Save.box_accumulated, 0f);


        }


    }
}


