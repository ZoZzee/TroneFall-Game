using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private CharacterController _characterController;

    public Transform cameraTransform;

    public float moveSpeed;
    public float speedRotation;

    public float speedWalking;
    public float speedRunning;

    private float p_verticalVelocity;
    [SerializeField] private float p_gravity;

    private Vector3 _forvard;
    private Vector3 _right;

    [Header("Components")]
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private Transform _rayDot;

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



        Vector3 direction = (_forvard * vertical + _right * horizontal);
        direction.y = 0f;
        direction.Normalize();

        if (_characterController.isGrounded)
        {
            p_verticalVelocity = -1f;
        }
        else
        {
            p_verticalVelocity += p_gravity * Time.deltaTime;
        }

        Vector3 velocity = direction * moveSpeed;
        velocity.y = p_verticalVelocity;

        _characterController.Move(velocity * Time.deltaTime);





        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = speedRunning;
            
        }
        else
        {
            moveSpeed = speedWalking;
        }

        if (direction.magnitude > 0.2f)
        {
           Quaternion targetRotation = Quaternion.LookRotation(direction);
           transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation);
               _animatorController.run = true;//Animator
        }
        else
        {
            _animatorController.run = false;//Animator
        }

    }
}
