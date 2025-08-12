using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Attack();

    public void Move()
    {
        Debug.Log("");
    }
}
