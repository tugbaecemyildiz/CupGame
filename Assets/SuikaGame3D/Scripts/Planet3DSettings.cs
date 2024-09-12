using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planet3DSetting", fileName = "Planet3DSetting", order = 0)]
public class Planet3DSettings : ScriptableObject
{
    [SerializeField] private Planet3DObject prefab;
    [SerializeField] private List<Texture> textures;
    [SerializeField] private List<float> scales;
    [SerializeField] private List<float> radius;

    [SerializeField] private Texture bonusTexture;
    [SerializeField] private float bonusScale;
    [SerializeField] private float bonusRadius;

    public Planet3DObject SpawnObject => prefab;
    public int MeshCount => textures.Count;

    public Texture GetTexture(int index)
    { 
        return textures[index];
    }
    public float GetScale(int index)
    {
        return scales[index];
    }

    public float GetRadius(int index)
    {
        return radius[index];
    }
    public Texture GetBonusTexture()
    {
        return bonusTexture;
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

