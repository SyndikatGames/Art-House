using System;
using UnityEngine;
using UnityEngine.UI;


namespace PrizeClaw
{
    public class ClawInput : MonoBehaviour
    {
        public static bool Enabled { get; set; } = true;

        [SerializeField] private Claw _manipulator;

        [SerializeField] private GameObject _mobileControlPanel;
        [SerializeField] private GameObject _desctopControlPanel;

        [SerializeField] private Button _moveLeftMobileButton;
        [SerializeField] private Button _moveRightMobileButton;
        [SerializeField] private Button _clawInteractMobileButton;


        private void Start()
        {
            _moveLeftMobileButton.onClick.AddListener(OnLeftMobileButtonClicked);
            _moveRightMobileButton.onClick.AddListener(OnRightMobileButtonClicked);
            _clawInteractMobileButton.onClick.AddListener(OnClawInteractMobileButtonClicked);

        }

        private void OnClawInteractMobileButtonClicked() => _manipulator.Interact();

        private void OnRightMobileButtonClicked() => _manipulator.Move(+1);

        private void OnLeftMobileButtonClicked() => _manipulator.Move(-1);

        private void Update()
        {
            if (!Enabled) return;

            float horizontalAxis = Input.GetAxis("Horizontal");
            if (horizontalAxis != 0f) _manipulator.Move(horizontalAxis);

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                _manipulator.Interact();


        }


    }
}



