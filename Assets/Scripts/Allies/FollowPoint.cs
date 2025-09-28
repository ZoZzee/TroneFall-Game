using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    [SerializeField] private Transform _targetToPlayer;
    private bool _playerNearby = false;
    private bool _pressFolov = false;
    private float _timer;
    [SerializeField]private float _maxTimer;


    private void LateUpdate()
    {
        if (_pressFolov)
        {
            transform.position = _targetToPlayer.position + new Vector3(0, 1, 0);
        }
        if(_playerNearby == false)
        {
            return;
        }
        if (Input.GetKey(KeyCode.R))
        {
            if (_maxTimer <= _timer)
            {
                _pressFolov = true;
            }
            else
            {
                _timer++;
            }
        }
        else
        {
            _timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            _pressFolov = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = true;
            _targetToPlayer = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerNearby = false;  
        }
    }


    }
