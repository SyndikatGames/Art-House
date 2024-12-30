using UnityEngine;
using VG2;
using Zenject;

public class SelectRoomStyleButton : ButtonHandler
{
    [SerializeField] private RoomStyleVariantView _styleVariant;

    [Inject] private RoomStyleController _roomStyleController;

    protected override void OnClick()
    {
        _roomStyleController.SetRoomStyle(_styleVariant.Index);
    }
}
