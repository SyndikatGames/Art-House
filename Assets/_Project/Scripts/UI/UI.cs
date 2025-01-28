using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    private static UI _instance;

    [SerializeField] private RectTransform _canvas; public static RectTransform Canvas => _instance._canvas;
    [SerializeField] private TextMeshProUGUI _moneyValue; public static TextMeshProUGUI MoneyValue => _instance._moneyValue;
    [SerializeField] private RectTransform _shop; public static RectTransform Shop => _instance._shop;




    private void Awake()
    {
        _instance = this;
    }



}
