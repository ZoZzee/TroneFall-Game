using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float attackCuldown;
    public float distanceToAttack;
    [HideInInspector] public float distanceToTarget;

    [SerializeField] private float _checkDistanceTime;

    [Header("References")]
    [SerializeField] private EnemyController enemyController;

    private void Start()
    {
        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
    }

    private IEnumerator CheckDistance()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkDistanceTime);
            distanceToTarget = Vector3.Distance(enemyController.target.position, transform.position);
        }
    }

    private IEnumerator AttackTimer()
    {
        while (true)
        {
            if (enemyController.targetHealth && distanceToTarget <= distanceToAttack)
            {
                enemyController.targetHealth.MinusHp(damage);
            }

            yield return new WaitForSeconds(attackCuldown);
        }
    }
}
