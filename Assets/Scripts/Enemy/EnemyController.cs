using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject _mainBild;
    [SerializeField] private float _speed;
    [SerializeField] private float _dictanse;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damageToPlayer;

    [SerializeField] private Vector3 _targetPosition;
    

    private PlayerController _playerController;

    private float timer;
    
    private bool _canAtack;
    private void Start()
    {
        _targetPosition = _mainBild.transform.position;
        _playerController = PlayerController.instance;
        _canAtack = false;
    }


    private void Update()
    {
        if ( ((_targetPosition.x - transform.position.x) >= _dictanse || (_targetPosition.x - transform.position.x) <= -_dictanse )
           || ((_targetPosition.z - transform.position.z) >= _dictanse || (_targetPosition.z - transform.position.z) <= -_dictanse))
        {
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
        else
        {
            AtackTarget(_damageToPlayer);
        }
    }

    private void AtackTarget(int count)
    {
        if (Time.time - timer >= attackCooldown)
        {
            _playerController.MinusHp(count);
            timer = Time.time;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _targetPosition = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetPosition = _mainBild.transform.position;
        }
    }


}
