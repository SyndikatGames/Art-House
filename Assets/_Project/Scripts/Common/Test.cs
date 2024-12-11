using UnityEngine;

public class Test : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            foreach (var rarityItemList in Prefabs.GetAllRaritySortedItems())
            {
                print($"{rarityItemList.Key}: {rarityItemList.Value.Count}");
            }



        }
    }
}
