using UnityEngine;
using VG2;

public class SoundSource : MonoBehaviour
{
    [SerializeField] private string _soundKey;


    private void Start()
    {
        Sound.Play(_soundKey);
    }


}
