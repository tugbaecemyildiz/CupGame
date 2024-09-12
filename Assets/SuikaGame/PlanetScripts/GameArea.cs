using UnityEngine;

public class GameArea : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Transform down;
    [SerializeField] private Transform up;

    public Vector2 GetBorderPositionHorizontal()
    {
        float leftWall = left.position.x + left.localScale.x;
        float rightWall = right.position.x - right.localScale.x;

        return new Vector2(leftWall, rightWall);
    }

    public Vector2 GetBorderPositionVertical()
    {
        return new Vector2(down.position.y, up.position.y);
    }
}
