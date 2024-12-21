using UnityEngine;

[System.Serializable]
public struct CardStyle
{
    public RarityType rarityType;
    public Sprite panelSprite;
    public Color textColor;



    public static float GetCardScaleForBottomPanel(Sprite itemSprite)
    {
        float maxSide = Mathf.Max(itemSprite.rect.width, itemSprite.rect.height);
        if (maxSide < 150f) return 0.7f;
        else if (maxSide < 250f) return 0.8f;
        else if (maxSide < 350f) return 0.9f;
        return 0.95f;
    }

    public static float GetCardScaleForBoxOpening(Sprite itemSprite)
    {
        float maxSide = Mathf.Max(itemSprite.rect.width, itemSprite.rect.height);
        if (maxSide < 150f) return 0.6f;
        else if (maxSide < 250f) return 0.7f;
        else if (maxSide < 350f) return 0.85f;
        return 1f;
    }



}
