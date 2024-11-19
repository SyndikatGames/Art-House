using System.Collections.Generic;
using UnityEngine;
using VG;

public class BoxSpawner : MonoBehaviour
{
    [System.Serializable]
    private struct RarityBox
    {
        public RarityType rarityType;
        public Box boxPrefab;
    }


    [SerializeField] private Transform _spawnTransform;
    [SerializeField] private List<RarityBox> _boxPrefabs;

    private List<Box> _spawnedBoxes = new List<Box>();
    private Dictionary<RarityType, int> _boxAmounts = new Dictionary<RarityType, int>();


    private void OnEnable()
    {
        Saves.String[Key_Save.boxes_data(0)].onChanged += OnBoxAmountChanged;
        foreach (var rarityType in EnumData.GetRarityTypes())
            _boxAmounts.Add(rarityType, 0);

        OnBoxAmountChanged();   
    }

    private void OnDisable()
    {
        Saves.String[Key_Save.boxes_data(0)].onChanged -= OnBoxAmountChanged;
    }


    private void OnBoxAmountChanged()
    {
        foreach (var rarityType in EnumData.GetRarityTypes())
        {
            int amount = Saves.GetBoxes(rarityType);

            var boxPrefab = _boxPrefabs.Find
                ((rarityBox) => rarityBox.rarityType == rarityType).boxPrefab;

            for (int i = _boxAmounts[rarityType]; i < amount; i++)
            {
                var boxInstance = Instantiate(boxPrefab, _spawnTransform);
                boxInstance.onOpened += () => _boxAmounts[rarityType]--;
            }

            _boxAmounts[rarityType] = amount;
        }
            


    }



}
