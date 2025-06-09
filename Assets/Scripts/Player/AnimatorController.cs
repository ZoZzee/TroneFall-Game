using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public string velocityName = "Velocity";
    public float velocity;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(velocityName, velocity);
    }
}
