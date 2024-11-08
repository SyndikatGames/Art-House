namespace VG
{
    public partial class Saves
    {

        public static int GetBoxesAmount()
        {
            int amount = 0;

            foreach (var rarityType in EnumData.GetRarityTypes())
                amount += Int[Key_Save.boxes_amount(rarityType)].Value;

            return amount;
        }

    }
}



