using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassManager : MonoBehaviour
{
    [SerializeField] private List<MoveGlass> _moveGlasses;
    [SerializeField] private Transform _ballTransform;
    [SerializeField] private ShuffleManager _shuffleManager;
    public void DownAllGlasses()
    {
        foreach (var glass in _moveGlasses)
        {
            glass.GlassDown();
        }
        StartCoroutine(ChangeBallParent());
    }

    public void UpAllGlasses()
    {
        foreach (var glass in _moveGlasses)
        {
            glass.GlassUp();
        }
    }
    public IEnumerator ChangeBallParent()
    {
        yield return new WaitForSeconds(1.2f);
        _ballTransform.SetParent(_moveGlasses[2].transform);
        if (_shuffleManager != null)
        {
            _shuffleManager.Shuffle();
        }
    }
}
