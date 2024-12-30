using VG2;
using Zenject;


public class OpenStylesWindowButton : ButtonHandler
{
    [Inject] private RoomStyleController _roomStyleController;


    protected override void OnClick()
    {
        _roomStyleController.OpenRoomStylesWindow();
    }
    
}