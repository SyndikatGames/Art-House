using VG2;


public class OpenRoomInfo_Button : ButtonHandler
{
    
    protected override void OnClick() 
        => Instantiate(Prefabs.RoomWindow, UI.Canvas).OpenInfo();

    
}