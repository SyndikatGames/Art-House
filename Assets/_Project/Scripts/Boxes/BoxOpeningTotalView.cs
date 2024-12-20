using UnityEngine;

public class BoxOpeningTotalView : MonoBehaviour
{
    [SerializeField] private ItemCard _cardPrefab;
    [SerializeField] private Transform _container;   

    public void Display(BoxOpeningModel boxOpeningModel)
    {
        foreach (Transform child in _container)
            Destroy(child.gameObject);




    }




}
