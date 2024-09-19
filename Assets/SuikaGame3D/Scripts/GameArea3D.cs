using UnityEngine;

public class GameArea3D : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    public Vector2 GetBorderPositionHorizontal()
    {
        float leftWall = left.position.x - left.localScale.x / 6;
        float rightWall = right.position.x - right.localScale.x / 1.5f;

        return new Vector2(leftWall, rightWall);
    } 
}
