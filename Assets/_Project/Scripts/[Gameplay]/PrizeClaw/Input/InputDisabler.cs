using UnityEngine;

namespace PrizeClaw
{
    public class InputDisabler : MonoBehaviour
    {

        private void OnEnable() => ClawInput.Enabled = false;

        private void OnDisable() => ClawInput.Enabled = true;

    }
}


