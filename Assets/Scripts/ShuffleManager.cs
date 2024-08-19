using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    [SerializeField] private List<MoveGlass> _moveGlasses;
    private int _firstIndex, _secondIndex;
    public float shuffleDuration = 0.5f;

    public void Shuffle()
    {
        StartCoroutine(ShuffleTimer());
    }
    private IEnumerator ShuffleTimer()
    {
        for (int i = 0; i < 5; i++)
        {
            _firstIndex = Random.Range(0, _moveGlasses.Count);
            do
            {
                _secondIndex = Random.Range(0, _moveGlasses.Count);
            } while (_firstIndex == _secondIndex);

            SwapGlasses();
            yield return new WaitForSeconds(shuffleDuration);
        }
    }

    private void SwapGlasses()
    {
        Transform firstGlass = _moveGlasses[_firstIndex].transform;
        Transform secondGlass = _moveGlasses[_secondIndex].transform;

        Vector3 tempPosition = firstGlass.position;

        firstGlass.DOMove(secondGlass.position, shuffleDuration).SetEase(Ease.InOutQuad);
        secondGlass.DOMove(tempPosition, shuffleDuration).SetEase(Ease.InOutQuad);
    }



}
