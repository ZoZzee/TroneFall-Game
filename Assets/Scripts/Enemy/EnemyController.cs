using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    [HideInInspector] public HealthManager targetHealth;

    [SerializeField] private float _speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damageToPlayer;

    [HideInInspector] public MainBuilding mainBuilding;
    [HideInInspector] public Transform mainBuildingTransform;

    [HideInInspector] public float distanceToTarget;

    [Header("References")]
    [SerializeField] private EnemyAttack enemyAttack;
    private EnemyManager _enemyManager;

    private void Start()
    {
        mainBuilding = BuildingsManager.instance.mainBuilding;
        mainBuildingTransform = mainBuilding.transform;
        _enemyManager = EnemyManager.instance;
        _enemyManager.acriveEnemy.Add(this.gameObject);
        SetMainBuildingAsTarget();
    }

    private void Update()
    {
        if (enemyAttack.distanceToTarget > enemyAttack.distanceToAttack - 0.3f)
        {
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    public void SetMainBuildingAsTarget()
    {
        target = mainBuildingTransform;
        targetHealth = mainBuilding.healthManager;
    }
}
