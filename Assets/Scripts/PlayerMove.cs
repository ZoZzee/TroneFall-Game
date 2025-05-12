using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;

    public Transform cameraTransform;

    public float moveSpeed;
    public float speedRotation;
    public Vector3 forvard;
    public Vector3 right;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        forvard = cameraTransform.forward;
        right = cameraTransform.right;

        forvard.y = 0;
        right.y = 0;


        forvard.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forvard * vertical + right * horizontal).normalized;


        if(moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation,targetRotation,speedRotation);
        }
    }
}
