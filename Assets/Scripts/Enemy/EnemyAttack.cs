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
            if (enemyController.target[0] != null)
            {
                distanceToTarget = Vector3.Distance(enemyController.target[0].position, transform.position);
            }
        }
    }

private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (enemyController.enemyAttack)
            {
                Debug.Log(enemyController.enemyAttack + " enemy Attack" );
                enemyController._animatorController.attack = true;
                enemyController.targetHealth[0].MinusHp(damage);
            }
            yield return new WaitForSeconds(attackCuldown);
            enemyController._animatorController.attack = false;

        }
    }
}
