using VG2;

public static class ShopCalculator
{
    
    public static bool ShopActionAvailable
    {
        get
        {
            foreach (var boxAmount in GameState.CurrentRoom.boxesAmount)
                if (boxAmount.Value > 0) return true;

            return false;
        }
    }



}
