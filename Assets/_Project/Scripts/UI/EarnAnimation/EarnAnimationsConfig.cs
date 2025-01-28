using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/EarnAnimations", fileName = "EarnAnimations")]
public class EarnAnimationsConfig : ScriptableObject
{
    [System.Serializable]
    public struct AnimationSettings
    {
        public EarnAnimationType animationType;
        public Sprite sprite;
        public int amount;
    }

    [SerializeField] private List<AnimationSettings> _animationSettings;

    public AnimationSettings GetSettings(EarnAnimationType animationType)
        => _animationSettings.Find(item => item.animationType == animationType);


}
