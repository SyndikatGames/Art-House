namespace VG
{
    public partial class Saves
    {

        public static int GetBoxes(RarityType rarityType)
        {
            int roomIndex = Int[Key_Save.current_room_index].Value;
            string[] splitBoxData = String[Key_Save.boxes_data(roomIndex)].Value.Split('_');

            return int.Parse(splitBoxData[(int)rarityType]);
        }

        public static void AddBoxes(RarityType rarityType, int amount)
        {
            int roomIndex = Int[Key_Save.current_room_index].Value;
            string[] data = String[Key_Save.boxes_data(roomIndex)].Value.Split('_');
            int value = int.Parse(data[(int)rarityType]);
            data[(int)rarityType] = (value + amount).ToString();

            String[Key_Save.boxes_data(roomIndex)].Value =
                $"{data[0]}_{data[1]}_{data[2]}_{data[3]}_{data[4]}_{data[5]}";
        }

        public static void RemoveBoxes(RarityType rarityType, int amount)
        {
            int roomIndex = Int[Key_Save.current_room_index].Value;
            string[] data = String[Key_Save.boxes_data(roomIndex)].Value.Split('_');
            int value = int.Parse(data[(int)rarityType]);
            data[(int)rarityType] = (value - amount).ToString();

            String[Key_Save.boxes_data(roomIndex)].Value =
                $"{data[0]}_{data[1]}_{data[2]}_{data[3]}_{data[4]}_{data[5]}";
        }


        public static int GetBoxesAmount()
        {
            int roomIndex = Int[Key_Save.current_room_index].Value;
            string[] data = String[Key_Save.boxes_data(roomIndex)].Value.Split('_');
            int result = 0;
            for (int i = 1; i < data.Length; i++)
                result += int.Parse(data[i]);   

            return result;
        }

    }
}



