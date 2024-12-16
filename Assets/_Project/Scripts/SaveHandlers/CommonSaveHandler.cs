namespace VG
{
    public partial class Saves
    {
        public bool PlayerIsNew => Int[Key_Save.tutorial_step].Value > 1;

    }
}


