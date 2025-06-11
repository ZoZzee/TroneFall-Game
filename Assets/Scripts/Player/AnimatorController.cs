using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public string velocityName = "Velocity";
    public float velocity;
    public string attackName = "Attack";
    public bool attack;
    public string deadName = "Dead";
    public bool dead;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetFloat(velocityName, velocity);

        _animator.SetBool(attackName, attack);

        _animator.SetBool(deadName, dead);
    }
}
