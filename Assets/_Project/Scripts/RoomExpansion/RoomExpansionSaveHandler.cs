using UnityEngine;

namespace VG
{
    public partial class Saves
    {
        public static Vector2Int RoomSize
        {
            get
            {
                var splitData = String[Key_Save.room_size_data(0)].Value.Split('_');
                return new Vector2Int(int.Parse(splitData[0]), int.Parse(splitData[1]));
            }
        }

        public static void SetRoomSize(Vector2Int size)
            => String[Key_Save.room_size_data(0)].Value = $"{size.x}_{size.y}";


    }
}

