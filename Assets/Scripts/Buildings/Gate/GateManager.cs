using UnityEngine;

public class GateManager : MonoBehaviour
{
    private Animator _animator;

    private string playerNearMe = "PlayerNear";
    private bool nearMe;
    //private Vector3 position;
    [SerializeField]private Collider _boxController;
    void Start()
    {
        //position = _boxController.transform.position;
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        nearMe = true;
        if (other.CompareTag("Player"))
        {
            //this._boxController.transform.position = new Vector3(0,0,0);
            _animator.SetBool(playerNearMe, nearMe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearMe = false;
        if (other.CompareTag("Player"))
        {

            //this._boxController.transform.Translate(position);
            
            _animator.SetBool(playerNearMe, nearMe);
        }
    }
}
