using UnityEngine;
using VG2;


namespace PrizeClaw
{
    public class PrizeSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2 _spawnAreaSize;


        private void Start() => Spawn();



        public void Spawn()
        {
            foreach (var prizeAmount in GameState.prizeClaw.spawnedPrizes)
            {
                for (int i = 0; i < prizeAmount.Value; i++)
                {
                    var prefab = ConfigHub.PrizeClaw.GetPrizePrefab(prizeAmount.Key);

                    var position = (Vector2)transform.position + new Vector2(
                        x: Random.Range(-_spawnAreaSize.x / 2f, _spawnAreaSize.x / 2f),
                        y: Random.Range(-_spawnAreaSize.y / 2f, _spawnAreaSize.y / 2f));

                    var rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));

                    Instantiate(prefab, position, rotation);
                }
            }

        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _spawnAreaSize);
        }



    }
}



