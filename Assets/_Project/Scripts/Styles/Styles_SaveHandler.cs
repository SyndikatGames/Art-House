
namespace VG
{
    public partial class Saves
    {
        public static bool StyleIsNew(int styleIndex)
        {
            var data = String[Key_Save.styles_is_new_data(0)].Value;
            return data[styleIndex] == '1';
        }

        public static void SetStyleNew(int styleIndex, bool isNew)
        {
            var charData = String[Key_Save.styles_is_new_data(0)].Value.ToCharArray();
            charData[styleIndex] = isNew ? '1' : '0';
            String[Key_Save.styles_is_new_data(0)].Value = new string(charData);
        }

    }
}

