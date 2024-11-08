using System.Collections.Generic;
using UnityEngine;
using VG;

public class BoxAccumulation : TimeHandler
{
    protected override string TimeSaveKey => Key_Save.time.box_accumulation;

    protected override void OnTimePassed(float seconds)
    {
        int boxLimit = TotalRules.BoxLimit;
        int hasBoxesAmount = Saves.GetBoxesAmount();

        if (hasBoxesAmount >= boxLimit)
            Saves.Float[Key_Save.box_accumulated].Value = 0f;

        else
        {
            float boxPerSecond = 1f / (TotalRules.BoxEveryMinutes * 60f);

            Saves.Float[Key_Save.box_accumulated].Value += boxPerSecond * seconds;
            float boxAccumulated = Saves.Float[Key_Save.box_accumulated].Value;

            if (boxAccumulated >= 1f)
            {
                int fullBoxes = (int)boxAccumulated;

                for (int i = 0; i < fullBoxes && hasBoxesAmount < boxLimit; i++, hasBoxesAmount++)
                {
                    var boxRarity = GenerateBoxRarity(TotalRules.BoxProbabilities);
                    Saves.Int[Key_Save.boxes_amount(boxRarity)].Value++;
                }
                if (hasBoxesAmount < boxLimit)
                    Saves.Float[Key_Save.box_accumulated].Value -= fullBoxes;

                else Saves.Float[Key_Save.box_accumulated].Value = 0f;
            }
        }
    }


    private RarityType GenerateBoxRarity(Dictionary<RarityType, float> probabilities)
    {
        float value = Random.Range(0f, 1f);
        float from = 0f;

        foreach (var probability in probabilities)
        {
            float to = from + probability.Value;

            if (from <= value && value <= to) 
                return probability.Key;
        }

        return RarityType.Common;
    }



}
