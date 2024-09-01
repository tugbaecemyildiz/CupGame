using System.Collections.Generic;
using UnityEngine;

public static class CardUtility
{
    public static List<MythologicalCharacter> GenerateCardFronts(List<MythologicalCharacter> cardFronts) 
    {
        List<MythologicalCharacter> selectedFronts = new List<MythologicalCharacter>();

        foreach (var front in cardFronts)
        {
            selectedFronts.Add(front);
            selectedFronts.Add(front);
        }
        return selectedFronts;
    }

    public static List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public static Transform[] ShuffleArray(Transform[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Transform temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
}
