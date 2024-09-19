using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    [SerializeField] private List<MoveGlass> _moveGlasses;
    private int _firstIndex, _secondIndex;
    private int _maxSuffleCount = 5;
    public float shuffleDuration = 0.3f;
    private bool _canShuffle = false;
    public bool isShuffling = true;

    public void Shuffle()
    {
        StartCoroutine(ShuffleTimer());
    }

    private IEnumerator ShuffleTimer()
    {
        for (int i = 0; i < _maxSuffleCount; i++)
        {
            _firstIndex = Random.Range(0, _moveGlasses.Count);
            _canShuffle = false;
            do
            {
                _secondIndex = Random.Range(0, _moveGlasses.Count);
            } while (_firstIndex == _secondIndex);

            SwapGlasses();

            while (!_canShuffle)
            {
                yield return null;
            }
        }
        isShuffling = false;
    }

    private void SwapGlasses()
    {
        GameObject cup1 = _moveGlasses[_firstIndex].gameObject;
        GameObject cup2 = _moveGlasses[_secondIndex].gameObject;

        Vector3 cup1Position = cup1.transform.position;
        Vector3 cup2Position = cup2.transform.position;

        // Bardak1: Yay þeklinde yukarý ve saða git
        float tempx = (cup1Position.x + cup2Position.x) / 2;
        Vector3[] path1 = new Vector3[]
        {
            new Vector3((cup1Position.x + cup2Position.x) / 2, cup1Position.y, cup1Position.z + 0.35f), cup2Position
        };
        cup1.transform.DOPath(path1, shuffleDuration, PathType.CatmullRom).SetEase(Ease.InOutQuad);

        // Bardak2: Yay þeklinde aþaðý ve sola git
        Vector3[] path2 = new Vector3[]
        {
            new Vector3((cup1Position.x + cup2Position.x) / 2, cup2Position.y, cup2Position.z - 0.35f), cup1Position
        };
        cup2.transform.DOPath(path2, shuffleDuration, PathType.CatmullRom).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                _canShuffle = true;
            });

    }
}