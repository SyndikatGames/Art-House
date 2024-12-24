using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Project/Room", fileName = "Room")]
public class RoomConfig : ScriptableObject
{
    [field: SerializeField] public List<float> PrestigeRequires { get; private set; }

}

public static partial class Configs
{
    
}



