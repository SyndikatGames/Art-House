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
        foreach (var rarityType in EnumData.GetRarityTypes())
        {
            Saves.Int[Key_Save.boxes_amount(rarityType)].onChanged += OnBoxAmountChanged;
            _boxAmounts.Add(rarityType, 0);
        }
        OnBoxAmountChanged();   
    }

    private void OnDisable()
    {
        foreach (var rarityType in EnumData.GetRarityTypes())
            Saves.Int[Key_Save.boxes_amount(rarityType)].onChanged -= OnBoxAmountChanged;
    }


    private void OnBoxAmountChanged()
    {
        foreach (var rarityType in EnumData.GetRarityTypes())
        {
            int amount = Saves.Int[Key_Save.boxes_amount(rarityType)].Value;

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
