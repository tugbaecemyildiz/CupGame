using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    [SerializeField] private GameArea3D gameArea;

    [SerializeField] private float _movementSpeed = 7f;
    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Yeni
        Vector3 hit = Planet3DManager.Instance.MousePositionHit();
        if (hit != Vector3.zero)
        {
            Vector2 xBounds = gameArea.GetBorderPositionHorizontal();

            float clampedX = Mathf.Clamp(hit.x, xBounds.x, xBounds.y);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
        }

        // --------------------------------

        //float moveX = Input.GetAxis("Horizontal");
        //Vector3 moveDirection = new Vector3(moveX, 0, 0).normalized;

        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    // Eski
    private void MoveWithJustMouseX()
    {
        float mouseX = Input.GetAxis("Mouse X");
        Vector3 movement = new Vector3(mouseX, 0, 0) * _movementSpeed * Time.deltaTime;
        transform.Translate(movement);

        Vector2 xBounds = gameArea.GetBorderPositionHorizontal();

        float clampedX = Mathf.Clamp(transform.position.x, xBounds.x, xBounds.y);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}