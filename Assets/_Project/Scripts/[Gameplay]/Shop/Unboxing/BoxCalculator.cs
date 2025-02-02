using UnityEngine;
using VG2;

public static class BoxCalculator
{
    


    public static int GetBoxesAmount(BoxType boxType)
    {
        if (GameState.CurrentRoom.boxesAmount.ContainsKey(boxType))
            return GameState.CurrentRoom.boxesAmount.Get(boxType);

        return 0;
    }


    public static void SetBoxAmount(BoxType boxType, int amount)
    {
        if (GameState.CurrentRoom.boxesAmount.ContainsKey(boxType) == false)
            GameState.CurrentRoom.boxesAmount.Add(boxType, amount);

        else GameState.CurrentRoom.boxesAmount.Set(boxType, amount);
    }

    public static void AddBoxes(BoxType boxType, int amount)
    {
        if (GameState.CurrentRoom.boxesAmount.ContainsKey(boxType) == false)
            GameState.CurrentRoom.boxesAmount.Add(boxType, amount);

        else
        {
            int oldAmount = GameState.CurrentRoom.boxesAmount.Get(boxType);
            GameState.CurrentRoom.boxesAmount.Set(boxType, oldAmount + amount);
        }
    }

    public static void RemoveBoxes(BoxType boxType, int amount)
    {
        if (GameState.CurrentRoom.boxesAmount.ContainsKey(boxType) == false)
            return;

        int oldAmount = GameState.CurrentRoom.boxesAmount.Get(boxType);
        int newAmount = Mathf.Max(0, oldAmount - amount);

        if (newAmount > 0) 
            GameState.CurrentRoom.boxesAmount.Set(boxType, newAmount);

        else GameState.CurrentRoom.boxesAmount.Remove(boxType);
    }




}
