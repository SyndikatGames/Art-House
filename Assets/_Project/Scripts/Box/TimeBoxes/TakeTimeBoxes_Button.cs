using VG;


public class TakeTimeBoxes_Button : ButtonHandler
{
    
    protected override void OnClick()
    {
        int roomIndex = Saves.Int[Key_Save.current_room_index].Value;
        int fullBoxes = (int)Saves.Float[Key_Save.random_boxes(roomIndex)].Value;

        for (int i = 0; i < fullBoxes; i++)
        {
            var boxRarity = TotalRules.GenerateTimeBoxRarity();
            Saves.AddBoxes(boxRarity, 1);
        }
        Saves.Float[Key_Save.random_boxes(roomIndex)].Value -= fullBoxes;

    }



    
}