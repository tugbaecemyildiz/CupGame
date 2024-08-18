using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuffleManager : MonoBehaviour
{
    [SerializeField] private List<MoveGlass> _moveGlasses;
    private int _firstIndex, _secondIndex;
    public void Shuffle()
    {
        _firstIndex = Random.Range(0,_moveGlasses.Count);
        do
        {
            _secondIndex = Random.Range(0, _moveGlasses.Count);
        } while (_firstIndex == _secondIndex);
        

    }
}
