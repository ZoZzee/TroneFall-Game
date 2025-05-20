using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody _rb;

    public Transform cameraTransform;

    public float moveSpeed;
    public float speedRotation;
    private Vector3 _forvard;
    private Vector3 _right;

    public static PlayerMove instance;

    private void Awake()
    {
        instance = this;

        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _forvard = cameraTransform.forward;
        _right = cameraTransform.right;

        _forvard.y = 0;
        _right.y = 0;

        _forvard.Normalize();
        _right.Normalize();

        Vector3 moveDirection = (_forvard * vertical + _right * horizontal).normalized;

        if(moveDirection.magnitude > 0.1f)
        {
            _rb.MovePosition(_rb.position + moveDirection * moveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _rb.rotation = Quaternion.Slerp(_rb.rotation,targetRotation,speedRotation);
        }
    }
}
