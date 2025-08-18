using UnityEngine;

public class AlliesBot : MonoBehaviour
{
    private IalliesState _currentState;

    public Transform target;
    public Vector3 spawnPoint;
    public float speed;
    public float rotationSpeed;
    public float distanseToAttack;

    private void OnEnable()
    {
        spawnPoint = transform.position;

        //SwitchState();
    }

    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
    public void SwitchState(IalliesState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        //_currentState.Enter(this);
    }
}
