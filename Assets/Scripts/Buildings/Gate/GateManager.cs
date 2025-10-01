using UnityEngine;

public class GateManager : MonoBehaviour
{
    private Animator _animator;

    private string playerNearMe = "PlayerNear";
    private bool nearMe;
    [SerializeField]private Collider _boxController;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        nearMe = true;
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies"))
        {
            _animator.SetBool(playerNearMe, nearMe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearMe = false;
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies"))
        {
            _animator.SetBool(playerNearMe, nearMe);
        }
    }
}
