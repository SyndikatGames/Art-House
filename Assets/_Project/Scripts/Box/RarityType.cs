using System.Collections.Generic;
using System;

public enum RarityType
{
    Shabby = 0, 
    Common = 1, 
    Rare = 2,
    Epic = 3,
    Fantastic = 4,
    Legendary = 5,
}

public static partial class EnumData
{
    private static List<RarityType> _allRarityTypes;

    public static List<RarityType> GetRarityTypes()
    {
        if (_allRarityTypes == null)
        {
            _allRarityTypes = new List<RarityType>();
            string[] names = Enum.GetNames(typeof(RarityType));

            foreach (string name in names)
                _allRarityTypes.Add((RarityType)Enum.Parse(typeof(RarityType), name));
        }

        return _allRarityTypes;
    }

}