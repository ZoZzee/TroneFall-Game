using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] public AnimationCurve speedCurve;

    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    private int oneGold = 1;

    private float _flyTime;
    [SerializeField] private float _flySpeed;

    private bool goo = false;
    private void LateUpdate()
    {
        if (goo)
        {
            Vector3 targetPosition = _target.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _speed);
            transform.position = smoothedPosition;

            _speed = speedCurve.Evaluate(_flyTime);
            _flyTime += _flySpeed;
            _flyTime = Mathf.Clamp(_flyTime, 0f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            goo = true;

            //FoloveTarget();
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
