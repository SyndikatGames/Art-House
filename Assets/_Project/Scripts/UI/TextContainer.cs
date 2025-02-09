using TMPro;
using UnityEngine;

public class TextContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    

    public string Text
    {
        get => _text.text;
        set => _text.text = value;
    }

}
