using UnityEngine;
using VG;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            var size = Saves.RoomSize;
            Saves.SetRoomSize(new Vector2Int(size.x + 1, size.y + 1));



        }
    }
}
