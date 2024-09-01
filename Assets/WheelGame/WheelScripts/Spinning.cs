using DG.Tweening;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float rotationDuration = 2f;
    [SerializeField] private NeedleController _needlect;
    public bool canSpin = true;

    public void RotationWheel() //360-1440 aras�nda d�nd�r
    {
        if (canSpin)
        {
            canSpin = false;
            float randomRotation = Random.Range(360f, 1440f) + 360f;
            transform.DORotate(new Vector3(0, 0, -randomRotation), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                _needlect.SpinningEnded();
                canSpin = true;
            });

        }
    }
}