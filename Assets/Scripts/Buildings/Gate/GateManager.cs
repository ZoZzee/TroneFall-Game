using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private string playerNearMe = "PlayerNear";
    private bool nearMe;
    [SerializeField]private Collider _boxController;
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies"))
        {
            nearMe = true;
            _animator.SetBool(playerNearMe, nearMe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies"))
        {
            nearMe = false;
            _animator.SetBool(playerNearMe, nearMe);
        }
    }
}
