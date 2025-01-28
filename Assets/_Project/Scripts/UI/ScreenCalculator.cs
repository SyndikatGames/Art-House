using UnityEngine;


public static class ScreenCalculator
{
    public static Vector2 GetScreenCenter() => new Vector2(Screen.width / 2f, Screen.height / 2f);
}
