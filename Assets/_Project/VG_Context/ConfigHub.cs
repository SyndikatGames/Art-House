using PrizeClaw;
using UnityEngine;

namespace VG2
{
    public static class ConfigHub
    {
        public static ShopConfig Shop => Resources.Load<ShopConfig>("Shop");
        public static BaseValuesConfig BaseValues => Resources.Load<BaseValuesConfig>("BaseValues");
        public static CardsConfig Cards => Resources.Load<CardsConfig>("Cards");
        public static UnboxingConfig Unboxing => Resources.Load<UnboxingConfig>("Unboxing");
        public static RoomConfig Room => Resources.Load<RoomConfig>("Room");
        public static PrizeClawConfig PrizeClaw => Resources.Load<PrizeClawConfig>("PrizeClaw");
        public static IncomeConfig Income => Resources.Load<IncomeConfig>("Income");
        public static TasksConfig Tasks => Resources.Load<TasksConfig>("Tasks");
        public static EarnAnimationsConfig EarnAnimations => Resources.Load<EarnAnimationsConfig>("EarnAnimations");
    }

}



