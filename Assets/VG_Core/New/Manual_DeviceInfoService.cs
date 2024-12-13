using UnityEngine;

namespace VG
{
    public class Manual_DeviceInfoService : DeviceInfoService
    {
        [SerializeField] private ControlType _deviceType;

        public override bool supported => true;

        public override ControlType GetDeviceType() => _deviceType;

        public override void Initialize() => InitCompleted();
    }
}

