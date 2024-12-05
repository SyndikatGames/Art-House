using TMPro;
using UnityEngine;


namespace PrizeClaw
{
    public class RewardIcon : MonoBehaviour
    {
        [field:SerializeField] public PrizeType PrizeType { get; private set; }

        [SerializeField] private TextMeshProUGUI _amountText;


        public void SetAmount(int amount) => _amountText.text = amount.ToString();

    }
}


