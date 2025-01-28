using UnityEngine;

public class CardInventoryInput : MonoBehaviour
{
    [SerializeField] private GameObject _scroller;
    [SerializeField] private HoldingButton _leftButton;
    [SerializeField] private HoldingButton _rightButton;
    [SerializeField] private RectTransform _contentRect;
    [SerializeField] private float _moveSpeed;


    private void Awake()
    {
        _leftButton.onHolding += MoveLeft;
        _rightButton.onHolding += MoveRight;
    }

    private void MoveRight() => Move(-1);

    private void MoveLeft() => Move(+1);


    private void Move(float direction)
    {
        var position = _contentRect.position;
        position.x += direction * _moveSpeed * Time.deltaTime;
        _contentRect.position = position;
    }


    private void Update()
    {
        bool showInput = _scroller.activeInHierarchy;
        _leftButton.gameObject.SetActive(showInput);
        _rightButton.gameObject.SetActive(showInput);
    }




}
