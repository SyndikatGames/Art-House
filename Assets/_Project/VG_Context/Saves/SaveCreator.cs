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

            new ItemBool(Key_Save.tutorial_completed, false);
            new ItemInt(Key_Save.tutorial_step, 0);

            new ItemFloat(Key_Save.soft_money, 500);
            new ItemInt(Key_Save.current_room_index, 0);
            


            for (int i = 0; i < roomsAmount; i++)
            {
                new ItemFloat(Key_Save.random_boxes(i), 10);
                new ItemString(Key_Save.boxes_data(i), "0_0_0_0_0_0");
                new ItemString(Key_Save.room_data(i), string.Empty);
                new ItemFloat(Key_Save.offline_time_seconds(i), 0f);
            }

        }


    }
}


