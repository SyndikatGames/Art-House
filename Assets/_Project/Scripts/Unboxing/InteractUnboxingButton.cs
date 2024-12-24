using VG2;
using Zenject;

public class InteractUnboxingButton : ButtonHandler
{
    [Inject] private UnboxingController _unboxingController;

    protected override void OnClick()
    {
        _unboxingController.Interact();
    }
}
