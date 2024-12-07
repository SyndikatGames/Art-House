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
                GameState.Current.AddReward(prize.PrizeType, 1);
                GameState.Current.RemovePrize(prize.PrizeType);
            }
        }


    }
}

