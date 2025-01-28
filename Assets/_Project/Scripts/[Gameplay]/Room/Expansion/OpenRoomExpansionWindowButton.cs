using VG2;
using Zenject;

public class OpenRoomExpansionWindowButton : ButtonHandler
{
    [Inject] private RoomExpansionController _roomExpansionController;

    protected override void OnClick()
    {
        _roomExpansionController.OpenRoomExpansionWindow();
    }

}
