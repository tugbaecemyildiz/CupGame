using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet3DSetting", fileName = "Planet3DSetting", order = 0)]
public class Planet3DSettings : ScriptableObject
{
    [SerializeField] private List<GameObject> prefabs;
    [SerializeField] private List<float> scales;

    [SerializeField] private GameObject bonusPrefab;
    [SerializeField] private float bonusScale;

    public int PrefabCount => prefabs.Count;

    public GameObject GetPrefab(int index)
    {
        return prefabs[index];
    }

    public float GetScale(int index)
    {
        return scales[index];
    }

    public GameObject GetBonusPrefab()
    {
        return bonusPrefab;
    }

    public float GetBonusScale()
    {
        return bonusScale;
    }
}

