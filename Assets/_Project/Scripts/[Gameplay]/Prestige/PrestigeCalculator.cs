using UnityEngine;
using VG2;

public static class PrestigeCalculator
{

    public static float GetCurrentRoomPrestigePoints()
    {
        float roomLevel = GetCurrentRoomLevel();

        float totalDoneRequire = 0f;
        for (int level = 1; level < roomLevel; level++)
            totalDoneRequire += ConfigHub.BaseValues.GetRoomLevelPrestigePointsRequire(level);

        return GetCurrentRoomTotalPrestigePoints() - totalDoneRequire;
    }

    public static float GetCurrentRoomPrestigeRequire()
    {
        int currentLevel = GetCurrentRoomLevel();
        return ConfigHub.BaseValues.GetRoomLevelPrestigePointsRequire(currentLevel);
    }


    public static int GetCurrentRoomLevel()
    {
        int level = 1;
        float prestigePoints = GetCurrentRoomTotalPrestigePoints();
        float levelRequire = ConfigHub.BaseValues.GetRoomLevelPrestigePointsRequire(level);
        while (prestigePoints >= levelRequire)
        {
            prestigePoints -= levelRequire;
            level++;
            levelRequire = ConfigHub.BaseValues.GetRoomLevelPrestigePointsRequire(level);
        }

        return level;
    }


    private static float GetCurrentRoomTotalPrestigePoints()
    {
        var placedItemsHierarchy = GameState.CurrentRoom.placedItemsHierarchy;

        float totalPrestigePoints = 0;
        foreach (var placedItemModel in placedItemsHierarchy)
            totalPrestigePoints += GetItemPrestigePointsRecursive(placedItemModel);

        return totalPrestigePoints;
    }


    private static float GetItemPrestigePointsRecursive(PlacedItemModel model)
    {
        float itemPrestigePoints = ConfigHub.BaseValues.GetItemPrestigePoints(model.rarityType);

        if (model.childItems == null || model.childItems.Count == 0)
            return itemPrestigePoints;

        float childPrestigePoints = 0f;
        foreach (var childItem in model.childItems)
            childPrestigePoints += GetItemPrestigePointsRecursive(childItem);

        return itemPrestigePoints + childPrestigePoints;

    }


}
