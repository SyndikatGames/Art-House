using System.Collections.Generic;
using UnityEngine;
using VG;


public class StylesWindow : Info
{
    public static string Name => "Styles(Clone)";

    [SerializeField] private List<StyleVariant> _styleVariants;


    protected override void Subscribe()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged += UpdateValue;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.Int[Key_Save.current_style_index(0)].onChanged -= UpdateValue;
        Saves.String[Key_Save.styles_is_new_data(0)].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
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
