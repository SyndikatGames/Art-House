using UnityEngine;


namespace PrizeClaw
{
    public class ClawInput : MonoBehaviour
    {
        [SerializeField] private Claw _manipulator;



        private void Update()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            if (horizontalAxis != 0f) _manipulator.Move(horizontalAxis);


            if (Input.GetMouseButtonDown(0))
                _manipulator.Interact();


        }


    }
}



