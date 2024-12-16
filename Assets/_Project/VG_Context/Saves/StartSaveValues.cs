using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/StartSaveValues")]
    public class StartSaveValues : LoadableFromTable
    {
        [field: SerializeField] public Vector2Int RoomSize { get; private set; }


        public override void LoadData(Dictionary<string, Table> allTables)
        {
            

        }
    }
}


