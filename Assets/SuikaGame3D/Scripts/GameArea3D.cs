using UnityEngine;

public class GameArea3D : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform down;
    [SerializeField] private Transform up;
    [SerializeField] private Transform forward;
    [SerializeField] private Transform backward;

    public Vector2 GetBorderPositionHorizontal()
    {
        float leftWall = left.position.x - left.localScale.x / 6;
        float rightWall = right.position.x - right.localScale.x / 1.5f;

        return new Vector2(leftWall, rightWall);
    }

    public Vector2 GetBorderPositionDepth()
    {
        float forwardWall = forward.position.z + forward.localScale.z / 2;
        float backwardWall = backward.position.z + backward.localScale.z / 5;

        return new Vector2(forwardWall, backwardWall);
    }
}
