
using VG;

namespace PrizeClaw
{
    public enum PrizeType
    {
        Money,
        CommonBox,
        RareBox,
        EpicBox,

    }


    public static class PrizeReleaser
    {
        public static void Release(PrizeType prizeType, int amount)
        {
            switch (prizeType)
            {
                case PrizeType.Money:
                    Saves.Float[Key_Save.money].Value += amount;
                    break;

                case PrizeType.CommonBox:
                    Saves.AddBoxes(BoxType.Common, amount);
                    break;

                case PrizeType.RareBox:
                    Saves.AddBoxes(BoxType.Rare, amount);
                    break;

                case PrizeType.EpicBox:
                    Saves.AddBoxes(BoxType.Epic, amount);
                    break;
            }
        }
    }

}




