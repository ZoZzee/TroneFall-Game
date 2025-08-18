using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttack : MonoBehaviour
{
    public int damage;
    public float attackCuldown;
    public float distanceToAttack;
    [HideInInspector] public float distanceToTarget;

    [SerializeField] private float _checkDistanceTime;

    [Header("References")]
    [SerializeField] private EnemyController enemyController;
    private EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = EnemyManager.instance;
    }

    private void OnEnable()
    {
        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
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
                    for (int i = 0; i < enemyManager.activeEnemy.Count; i++)
                    {
                        Debug.Log("Зашло в перевірку");
                        EnemyController enemy = enemyManager.activeEnemy[i].GetComponent<EnemyController>();
                        enemy.target.Remove(enemyController.target[0]);
                        enemy.targetHealth.Remove(enemyController.targetHealth[0]);
                    }

                    Debug.Log("Або не зайшло");
                }
            }
            yield return new WaitForSeconds(0.1f);
            enemyController._animatorController.attack = false;
            yield return new WaitForSeconds(attackCuldown - 0.1f);

        }
    }
}
