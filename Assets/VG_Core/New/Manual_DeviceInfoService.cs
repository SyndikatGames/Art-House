using UnityEngine;

namespace VG
{
    public class Manual_DeviceInfoService : DeviceInfoService
    {
        [SerializeField] private DeviceType _deviceType;

        public override bool supported => true;

        public override DeviceType GetDeviceType() => _deviceType;

        public override void Initialize() => InitCompleted();
    }
}

