using UnityEngine;
using VG;

public abstract class ItemInteractionHandler : MonoBehaviour
{
    protected ItemInteraction itemInteraction;

    private Camera _camera;
    protected Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }


    public static ItemInteractionHandler GetHandler(GameObject interactableObject)
    {
        if (DeviceInfo.ControlType == ControlType.Desktop)
            return interactableObject.AddComponent<DesktopItemInteraction>();

        else return interactableObject.AddComponent<MobileItemInteraction>();
    }


    public void Initialize(ItemInteraction itemInteraction) 
        => this.itemInteraction = itemInteraction;

    public abstract void DisableDragging();


}
