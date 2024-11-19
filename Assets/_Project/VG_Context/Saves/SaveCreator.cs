using System;
using static VG.Saves;


namespace VG
{
    public static class SaveCreator
    {

        public static void Create(StartSaveValues startValues)
        {
            new ItemString(Key_Save.last_enter_time, DateTime.Now.ToString());
            new ItemBool(Key_Save.ads_enabled, true);

            new ItemInt(Key_Save.gems, 0);
            new ItemInt(Key_Save.current_room_index, 0);


            for (int i = 0; i < roomsAmount; i++)
            {
                new ItemFloat(Key_Save.time_boxes(i), 0f);
                new ItemString(Key_Save.boxes_data(i), "0_20_0_0_0_0");
                new ItemString(Key_Save.room_data(i), string.Empty);
            }

        }


    }
}


