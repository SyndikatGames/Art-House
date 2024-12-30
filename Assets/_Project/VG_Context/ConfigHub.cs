
namespace VG2
{
    public static class ConfigHub
    {

        private static CashedResource<ShopConfig> _shopCash  = new CashedResource<ShopConfig>("Shop");
        public static ShopConfig Shop => _shopCash.Value;


        private static CashedResource<BaseValuesConfig> _baseValuesCash = new CashedResource<BaseValuesConfig>("BaseValues");
        public static BaseValuesConfig BaseValues => _baseValuesCash.Value;


        private static CashedResource<CardsConfig> _cardsCash = new CashedResource<CardsConfig>("Cards");
        public static CardsConfig Cards => _cardsCash.Value;


        private static CashedResource<UnboxingConfig> _unboxingCash = new CashedResource<UnboxingConfig>("Unboxing");
        public static UnboxingConfig Unboxing => _unboxingCash.Value;


        private static CashedResource<RoomStylesConfig> _stylesCash = new CashedResource<RoomStylesConfig>("Styles");
        public static RoomStylesConfig RoomStyles => _stylesCash.Value;


    }

}



