using VG.Internal;


namespace VG
{
    public enum DeviceType { Desktop, Mobile }

    public class DeviceInfo : Manager
    {
        private static DeviceInfo instance;
        private static DeviceInfoService service => instance.supportedService as DeviceInfoService;

        protected override string managerName => "Device Definer";

        protected override void OnInitialized()
        {
            instance = this;
            Log(Core.Message.Initialized(managerName));
        }


        public static DeviceType DeviceType => service.GetDeviceType();


    }
}


