using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SellArea : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static SellArea _instance;

    [SerializeField] private GameObject _panel;
    [SerializeField] private Image _panelImage;

    private readonly Color highlightPanelColor = new Color(0f, 0.6f, 0f, 0.7f);
    private readonly Color originPanelColor = new Color(0f, 0.5f, 0f, 0.5f);

    public static void SetActive(bool value)
    {
        if (TutorialController.SellAreaEnabled)
            _instance._panel.SetActive(value);

        else _instance._panel.SetActive(false);
    }
    public static bool PointerInsideArea { get; private set; } = false;


    private void Awake()
    {
        _instance = this;
        _panel.SetActive(false);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerInsideArea = true;
        _panelImage.color = highlightPanelColor;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerInsideArea = false;
        _panelImage.color = originPanelColor;
    }



}
