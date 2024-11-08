

namespace VG
{
    public static class Key_Save
    {
        public static class time
        {
            public static string box_accumulation => "t_ba";
        }

        public static string box_accumulated => "bap";

        public static string test_count => "test";
        public static string ads_enabled => "ads";
        public static string gems => "g";


        public static string boxes_amount(RarityType rarityType) => $"b{(int)rarityType}";

        public static string room_data(int index) => $"r{index}";


    }
}



