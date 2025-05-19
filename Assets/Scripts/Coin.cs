using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float distanceOnStart;

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

            float distanceNow = Vector3.Distance(transform.position, targetPosition);
            float t = 1f - (distanceNow / distanceOnStart);
            t = Mathf.Clamp01(t);

            _speed = speedCurve.Evaluate(t);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            goo = true;
            distanceOnStart = Mathf.Max(Vector3.Distance(transform.position, _target.position), 0.001f);
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
