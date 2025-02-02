using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [field: SerializeField] public RectTransform CommonBoxButtonRect;

    public Image CommonBoxButtonImage => CommonBoxButtonRect.GetComponent<Image>();

    


}
