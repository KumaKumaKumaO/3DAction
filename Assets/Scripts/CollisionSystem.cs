using UnityEngine;
public class CollisionSystem
{
    public bool IsCollision(BaseObjectScript targetA, BaseObjectScript targetB)
    {
        if (targetA.MyColisionType == ColliderType.Sphere)
        {

        }
        return false;
    }
    public float GetRadius(BaseObjectScript target, Vector3 vector)
    {
        if (target.MyColisionType == ColliderType.Sphere)
        {
            //x2 + y2 + z2 + k*x + l*y + m*z + n = 0
        }
        else if (target.MyColisionType == ColliderType.Box)
        {

        }
        return 0;
    }
}