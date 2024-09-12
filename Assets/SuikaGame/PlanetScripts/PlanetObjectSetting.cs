using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlanetObjectSetting", fileName = "PlanetObjectSetting", order = 0)]
public class PlanetObjectSetting : ScriptableObject
{
    
    [SerializeField] private PlanetObject prefab;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<float> scales;
    [SerializeField] private List<float> radii;

    [SerializeField] private Sprite bonusSprite;
    [SerializeField] private float bonusScale;
    [SerializeField] private float bonusRadius;

    public PlanetObject SpawnObject => prefab;

    public int SpritesCount => sprites.Count;

    public Sprite GetSprite(int index)
    {
        return sprites[index];
    }

    public float GetScale(int index)
    {
        return scales[index];
    }

    public float GetRadius(int index)
    {
        return radii[index];
    } 

    public Sprite GetBonusSprite()
    {
        return bonusSprite;
    }

    public float GetBonusScale()
    {
        return bonusScale;
    }

    public float GetBonusRadius()
    {
        return bonusRadius;
    }
}
