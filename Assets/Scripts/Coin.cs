using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _timerAfterStartDay;

    private float _speedUp;
    [SerializeField] private float _speedUpAmount;

    [SerializeField] private float _maxSpeed;
    private int oneGold = 1;

    private float _timer;
    private bool _canMove;
    private DayNightManager dayNightManager;
    private void Start()
    {
        dayNightManager = DayNightManager.instance;
    }
    private void LateUpdate()
    {
        if (dayNightManager.dayStart && !_canMove)
        {
            _canMove = true;
            _timer = Time.time;
            _target = PlayerMove.instance.transform;
        }
        if (Time.time - _timer >= _timerAfterStartDay)
        {
                FollowingThePlayer();
        }
        
    }

    private void FollowingThePlayer()
    {
        Vector3 targetPosition = _target.position + Vector3.up;
        _speedUp += _speedUpAmount * Time.deltaTime;
        _speed = _baseSpeed + _speedUp;
        Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoldManager.instance.PlusGold(oneGold);
            Destroy(gameObject);
        }
    }

}
