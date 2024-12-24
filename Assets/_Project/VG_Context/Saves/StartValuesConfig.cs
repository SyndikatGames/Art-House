using System.Collections.Generic;
using UnityEngine;


namespace VG2
{
    [CreateAssetMenu(menuName = "VG/Start Values")]
    public class StartValuesConfig : LoadableFromTable
    {
        [field: SerializeField] public Vector2Int RoomSize { get; private set; }


        public override void LoadData(Dictionary<string, Table> allTables)
        {
            

        }
    }
}


