using UnityEngine;

public class SpawnerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    [SerializeField] private GameArea3D gameArea;

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        Vector2 xBounds = gameArea.GetBorderPositionHorizontal();
        Vector2 zBounds = gameArea.GetBorderPositionDepth();

        float x = Mathf.Clamp(transform.position.x,xBounds.x,xBounds.y);
        float z = Mathf.Clamp(transform.position.z,zBounds.x,zBounds.y);

        transform.position = new Vector3 (x,transform.position.y,z);
        
    }
}
