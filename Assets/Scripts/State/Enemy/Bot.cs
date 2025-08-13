using UnityEngine;

public class Bot : MonoBehaviour
{
    private IEnemyState _currentState;

    public Transform target;
    public Vector3 spawnPoint;
    public float speed;
    public float rotationSpeed;
    public float distanseToAttack;

    private void OnEnable()
    {
        spawnPoint = transform.position;

        SwitchState(new PatrolState());
    }

    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
    public void SwitchState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
    }

}
