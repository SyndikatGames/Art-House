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

        [SerializeField] private Button _moveLeftMobileButton;
        [SerializeField] private Button _moveRightMobileButton;
        [SerializeField] private Button _clawInteractMobileButton;


        private void Start()
        {
            _mobileControlPanel.SetActive(DeviceInfo.DeviceType == VG.DeviceType.Mobile);
            _desctopControlPanel.SetActive(DeviceInfo.DeviceType == VG.DeviceType.Desktop);

            if (DeviceInfo.DeviceType == VG.DeviceType.Mobile)
            {
                _moveLeftMobileButton.onClick.AddListener(OnLeftMobileButtonClicked);
                _moveRightMobileButton.onClick.AddListener(OnRightMobileButtonClicked);
                _clawInteractMobileButton.onClick.AddListener(OnClawInteractMobileButtonClicked);
            }
        }

        private void OnClawInteractMobileButtonClicked() => _manipulator.Interact();

        private void OnRightMobileButtonClicked() => _manipulator.Move(+1);

        private void OnLeftMobileButtonClicked() => _manipulator.Move(-1);

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



