using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Transform> target;
    public List<HealthManager> targetHealth;

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
        if (!enemyAttack && target[0] != null)
        {
            transform.LookAt(target[0]);
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].position, _speed * Time.deltaTime);
            transform.position = smoothedPosition;
            _animatorController.velocity = smoothedPosition.normalized.magnitude;
        }
        else
        {
            _animatorController.velocity = 0;
        }

    }

    public void SetMainBuildingAsTarget()
    {
        Debug.Log(" Перед додаванням " + target[0]);
        target[0] = mainBuildingTransform;
        targetHealth[0] = mainBuilding.healthManager;

        Debug.Log(" Add main " + target[0]);
    }

    
}
