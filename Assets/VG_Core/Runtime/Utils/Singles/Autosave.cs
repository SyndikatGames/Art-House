using UnityEngine;

namespace VG
{
    public class Autosave : MonoBehaviour
    {
        [SerializeField] private float _autosaveTimeInterval = 1f;

        private float _timeToAutosave;

        private void Start()
        {
            _timeToAutosave = _autosaveTimeInterval;
        }

        private void Update()
        {
            if (!Startup.Loaded) return;


            _timeToAutosave -= Time.unscaledDeltaTime;

            if (_timeToAutosave < 0f)
            {
                _timeToAutosave = _autosaveTimeInterval;
                Saves.Commit();
            }
        }


        private void OnApplicationFocus(bool focus)
        {
            if (!Startup.Loaded) return;

            if (!focus) Saves.Commit();
        }

        private void OnApplicationPause(bool pause)
        {
            if (!Startup.Loaded) return;

            if (pause) Saves.Commit();
        }

        private void OnApplicationQuit()
        {
            if (!Startup.Loaded) return;

            Saves.Commit();
        }




    }
}



