using DG.Tweening;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float rotationDuration = 2f;
    [SerializeField] private NeedleController _needlect;
    public bool canSpin = true;
    private float rotateDir = 1;
    [SerializeField] private Transform wheelHolder;


    public float spinSpeed = 10f;
    private bool isDragging = false;
    private Vector2 lastMousePosition;

    private void Start()
    {
        
    }
    public void RotationWheel()
    {
        GetComponent<Collider2D>().isTrigger = true;
        if (canSpin)
        {
            canSpin = false;
            float randomRotation = Random.Range(360f, 1440f) + 360f;
            randomRotation *= rotateDir;
            wheelHolder.DORotate(new Vector3(0, 0, randomRotation), rotationDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                _needlect.SpinningEnded();
                canSpin = true;
            });
        }
    }

    private void OnMouseDown()
    {
        isDragging = true;
        lastMousePosition = MousePos();
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 mouseDelta = MousePos() - lastMousePosition;
            float rotationAmount = mouseDelta.y * spinSpeed * Time.deltaTime;
            if (MousePos().x < 0)
            {
                wheelHolder.Rotate(0, 0, -rotationAmount);
            }
            if (MousePos().x > 0)
            {
                wheelHolder.Rotate(0, 0, rotationAmount);
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        if (lastMousePosition.y > MousePos().y)
        {
            if (lastMousePosition.x < 0)
                rotateDir = 1;
            else if(lastMousePosition.x > 0)
                rotateDir = -1;
        }
        else if (lastMousePosition.y < MousePos().y)
        {
            if (lastMousePosition.x < 0)
                rotateDir = -1;
            else if (lastMousePosition.x > 0)
                rotateDir = 1;
        }
        RotationWheel();
    }

    public Vector2 MousePos()
    {
        Vector2 pos = Input.mousePosition;
        pos.x -= Screen.width / 2;
        pos.y -= Screen.height / 2;
        return pos;
    }
}