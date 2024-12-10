using UnityEngine;
using UnityEngine.UI;
using VG;


namespace PrizeClaw
{
    public class ClawInput : MonoBehaviour
    {
        public static bool Enabled { get; set; } = true;

        [SerializeField] private Claw _manipulator;

        [SerializeField] private GameObject _mobileControlPanel;
        [SerializeField] private GameObject _desctopControlPanel;

        [SerializeField] private HoldingButton _moveLeftMobileButton;
        [SerializeField] private HoldingButton _moveRightMobileButton;
        [SerializeField] private Button _clawInteractMobileButton;


        private void Start()
        {
            _mobileControlPanel.SetActive(DeviceInfo.DeviceType == VG.DeviceType.Mobile);
            _desctopControlPanel.SetActive(DeviceInfo.DeviceType == VG.DeviceType.Desktop);

            if (DeviceInfo.DeviceType == VG.DeviceType.Mobile)
            {
                _moveLeftMobileButton.onHolding += OnLeftMobileButtonHolding;
                _moveRightMobileButton.onHolding += OnRightMobileButtonHolding;
                _clawInteractMobileButton.onClick.AddListener(OnClawInteractMobileButtonClicked);
            }
        }

        private void OnClawInteractMobileButtonClicked() => _manipulator.Interact();

        private void OnRightMobileButtonHolding() => _manipulator.Move(+1);

        private void OnLeftMobileButtonHolding() => _manipulator.Move(-1);


        private void Update()
        {
            if (Enabled) HandleDesctopInput();
        }

        private void HandleDesctopInput()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            if (horizontalAxis != 0f) _manipulator.Move(horizontalAxis);

            if (Input.GetKeyDown(KeyCode.Return))
                _manipulator.Interact();
        }



    }
}



