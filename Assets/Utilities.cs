using UnityEngine;
public static class Utilities
{

    public static Vector3 SetDirection(Direction dir, Vector3 position, float speed = 1)
    {
        switch (dir)
        {
            case Direction.up:
                {
                    return new Vector3(position.x, position.y + speed, position.z);
                }
            case Direction.down:
                {
                    return new Vector3(position.x, position.y - speed, position.z);
                }
            case Direction.left:
                {
                    return new Vector3(position.x - speed, position.y, position.z);
                }
            case Direction.right:
                {
                    return new Vector3(position.x + speed, position.y, position.z);
                }
            default: return position;

        }
    }
}