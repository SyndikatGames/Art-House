using System.Collections.Generic;

public static class MobileTouches
{
    public enum State { Free, MoveItem, MoveCamera, ScaleCamera }


    public static readonly Dictionary<int, State> TouchStates = new Dictionary<int, State>
    {
        { 0, State.Free },
        { 1, State.Free },
        { 2, State.Free },
    };




}
