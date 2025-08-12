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

    private void OnEnable()
    {
        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
    }
    private void OnDisable()
    {
        CancelInvoke();
    }

    private IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(0.2f);
        while (true && !enemyController._animatorController.dead)
        {
            yield return new WaitForSeconds(_checkDistanceTime);
            if (enemyController.target[0] != null)
            {
                distanceToTarget = Vector3.Distance(enemyController.target[0].transform.position, transform.position);
            }
        }
    }

private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1f);

        while (true && !enemyController._animatorController.dead)
        {
            if (enemyController.enemyAttack)
            {
                enemyController._animatorController.attack = true;
                enemyController.targetHealth[0].MinusHp(damage);
                if (enemyController.targetHealth[0]._health == 0f)
                {
                    Debug.Log("Знищення тавера");
                    enemyController.RefreshTarget();
                }
            }
            yield return new WaitForSeconds(0.1f);
            enemyController._animatorController.attack = false;
            yield return new WaitForSeconds(attackCuldown - 0.1f);

        }
    }
}
