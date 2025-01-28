using UnityEngine;


namespace PrizeClaw
{
    public class PrizeInputArea : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.attachedRigidbody.transform.TryGetComponent<Prize>(out var prize))
            {
                Destroy(prize.gameObject);
                RewardCalculator.AcceptPrize(prize.PrizeType);
            }
        }


    }
}

