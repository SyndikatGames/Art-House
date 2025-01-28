using VG2;
using Zenject;

public class ExpandRoomButton : ButtonHandler
{
    [Inject] private RoomExpansionController _roomExpansionConrtoller;


    protected override void OnClick()
    {
        _roomExpansionConrtoller.BuyNextRoomExpansion();
    }
}
