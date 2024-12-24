using UnityEngine;

namespace VG2
{
    public static class ConfigHub
    {

        public static RoomConfig GetRoom(int index) => Resources.Load<RoomConfig>($"Rooms/{index}");


    }

}



