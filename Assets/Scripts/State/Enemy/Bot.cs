using UnityEngine;

public class Bot : MonoBehaviour
{
    public Transform target;
    public Vector3 spawnPoint;
    public float speed;
    public float rotationSpeed;
    public float distanseToAttack;

    private void OnEnable()
    {
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
        
    }
    public void SwitchState(IEnemyState newState)
    {

    }

}
