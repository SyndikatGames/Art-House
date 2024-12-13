using VG.YandexGames;

namespace VG
{
    public class YandexGames_DeviceInfoService : DeviceInfoService
    {
        public override bool supported => 
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;

        public override ControlType GetDeviceType() => YG_Sdk.GetDeviceType();

        public override void Initialize() => InitCompleted();

    }
}

