using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spinning : MonoBehaviour
{
    public float rotationDuration = 2f;
    [SerializeField] private NeedleController _needlect;

    public void RotationWheel()
    {
        float randomRotation = Random.Range(360f, 1440f);
        transform.DORotate(new Vector3(0, 0, -randomRotation), rotationDuration, RotateMode.FastBeyond360).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _needlect.SpinningEnded();

        });
    }
}
