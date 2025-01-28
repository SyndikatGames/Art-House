using UnityEngine;

[CreateAssetMenu(menuName = "Project/Income", fileName = "Income")]
public class IncomeConfig : ScriptableObject
{
    [field: SerializeField] public IncomeWindowView IncomeWindow { get; private set; }
    [field: SerializeField] public float PutIncomeEverySeconds { get; private set; }

    [SerializeField] private float _startIncomePerHour;
    [SerializeField] private float _increaseIncomePerHourEveryRoomLevel;



    public float GetIncomePerHour(int roomLevel) => _startIncomePerHour + _increaseIncomePerHourEveryRoomLevel * (roomLevel - 1);



}
