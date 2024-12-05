

namespace VG
{
    public static class Key_Save
    {
        public static string last_enter_time => "last";

        public static string offline_time_seconds(int roomIndex) => $"off{roomIndex}";
        public static string random_boxes(int roomIndex) => $"tb{roomIndex}";

        public static string ads_enabled => "ads";
        public static string soft_money => "g";

        public static string tutorial_step => "ts";
        public static string tutorial_completed => "tc";


        public static string boxes_data(int roomIndex) => $"b{roomIndex}";

        public static string room_data(int index) => $"r{index}";

        public static string current_room_index => "cri";

        public static string prize_claw_data => "pcd";


    }
}



