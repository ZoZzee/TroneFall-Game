using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private CharacterController _characterController;

    public Transform cameraTransform;

    public float moveSpeed;
    public float speedRotation;

    public float speedWalking;
    public float speedRunning;

    private Vector3 _forvard;
    private Vector3 _right;

    [Header("Components")]
    [SerializeField] private AnimatorController _animatorController;

    public static PlayerMove instance;

    private void Awake()
    {
        instance = this;
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

        Vector3 moveDirection = (_forvard * vertical + _right * horizontal);
        Vector3 moveDirectionNormalized = moveDirection.normalized;
        float velocity = moveDirection.magnitude;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = speedRunning;
            velocity += 1f;
        }
        else
        {
            moveSpeed = speedWalking;
        }

        if (moveDirectionNormalized.magnitude > 0.1f)
        {
            //_rb.MovePosition(_rb.position + moveDirectionNormalized * moveSpeed);
            _characterController.Move(moveDirectionNormalized * moveSpeed);
            Quaternion targetRotation = Quaternion.LookRotation(moveDirectionNormalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation);
        }

        _animatorController.velocity = velocity;
    }
}
