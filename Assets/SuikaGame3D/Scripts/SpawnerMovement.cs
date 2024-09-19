using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    public float moveSpeed = 3f;
    [SerializeField] private GameArea3D gameArea;
    private void Awake()
    {
       mainCamera = Camera.main;
    }
    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(moveX, 0, 0).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        Vector2 xBounds = gameArea.GetBorderPositionHorizontal();

        float x = Mathf.Clamp(transform.position.x, xBounds.x, xBounds.y);

        transform.position = new Vector3(x, transform.position.y, transform.position.z);

    }

    private Vector3 GetInputHorizontalPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out RaycastHit hit, float.MaxValue, layerMask))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
}
