using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Transform> target;
    [HideInInspector] public List<HealthManager> targetHealth;

    [SerializeField] private float _speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damageToPlayer;

    [HideInInspector] public MainBuilding mainBuilding;
    [HideInInspector] public Transform mainBuildingTransform;

    [HideInInspector] public float distanceToTarget;
    [HideInInspector] public bool enemyAttack;

    [Header("References")]
    [SerializeField] private EnemyAttack _enemyAttack;
    private EnemyManager _enemyManager;


    [Header("Components")]
    public AnimatorController _animatorController;

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
        if (_enemyAttack.distanceToTarget > _enemyAttack.distanceToAttack - 0.3f && target[0] != null)
        {

            Debug.Log(" Дивиться " + target[0]);
            transform.LookAt(target[0]);
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].position, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
            _animatorController.velocity = smoothedPosition.normalized.magnitude;
        }

    }

    public void SetMainBuildingAsTarget()
    {
        Debug.Log(" Запис " + target[0]);
            
        if (target.Count <= 1 && target[0] == null)
        {
            
            target[0] = mainBuildingTransform;
            targetHealth[0] = mainBuilding.healthManager;

        }
        else
        {
            target.RemoveAt(0);
        }
    }
}
