using VG;
using Zenject;

public class SkipUnboxingButton : ButtonHandler
{
    [Inject] private UnboxingController _unboxingController;

    protected override void OnClick()
    {
        _unboxingController.Skip();
    }
}
