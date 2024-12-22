using System.Collections.Generic;
using UnityEngine;
using VG;


public class StylesWindow : ReactiveView
{
    public static string Name => "Styles(Clone)";

    [SerializeField] private List<StyleVariant> _styleVariants;


    protected override void Subscribe()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged += Display;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged += Display;
    }

    protected override void Dispose()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged -= Display;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged -= Display;
    }

    protected override void Display()
    {
        for (int i = 0; i < _styleVariants.Count; i++)
            _styleVariants[i].UpdateStyleIndex(i);

    }


}

public static partial class Prefabs
{
    public static StylesWindow StylesWindow
        => Resources.Load<StylesWindow>("Windows/Styles");
}
