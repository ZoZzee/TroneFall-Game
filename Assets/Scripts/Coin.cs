using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _baseSpeed;

    private float _speedUp;
    [SerializeField] private float _speedUpAmount;

    [SerializeField] private float _maxSpeed;
    private int oneGold = 1;

    private bool goo = false;

    private void LateUpdate()
    {
        if (goo)
        {
            Vector3 targetPosition = _target.position + Vector3.up;

            _speedUp += _speedUpAmount * Time.deltaTime;
            _speed = _baseSpeed + _speedUp;

            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            goo = true;
            _target = PlayerMove.instance.transform;
        }
    }


    //private void FoloveTarget()
    //{
    //    StartCoroutine(GoCoin());
    //}

    //private IEnumerator GoCoin()
    //{
    //    while(_speed <= _maxSpeed)
    //    {
    //        _speed += 0.1f;
    //        yield return new WaitForSeconds(0.1f);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GoldManager.instance.PlusGold(oneGold);
            Destroy(gameObject);
        }
    }

}
