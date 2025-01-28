using TMPro;
using UnityEngine;
using VG2;


namespace PrizeClaw
{
    public class RewardIcon : MonoBehaviour
    {
        [field:SerializeField] public PrizeType PrizeType { get; private set; }

        [SerializeField] private TextMeshProUGUI _amountText;


        public void SetAmount(int amount)
        {
            if (PrizeType == PrizeType.Money)
                _amountText.text = (amount * ConfigHub.PrizeClaw.MoneyInsideReward).ToShortNumber();

            else _amountText.text = amount.ToString();

        }

    }
}


