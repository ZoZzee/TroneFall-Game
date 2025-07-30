using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<Transform> target;
    public List<Transform> _duplicateTarget;
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

        SetMainBuildingAsTarget();

        _enemyManager = EnemyManager.instance;
        _enemyManager.activeEnemy.Add(this.gameObject);
    }
    

    private void Update()
    {
        if (target.Contains(_duplicateTarget[0]))
        {
            RefreshTarget();
        }
        if (!enemyAttack && target.Count > 0 && !_animatorController.dead)
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

    public void RefreshTarget()
    {
        target.RemoveAt(0);
        targetHealth.RemoveAt(0);
        _duplicateTarget.RemoveAt(0);
    }

    public void SetMainBuildingAsTarget()
    {
        target[0] = mainBuildingTransform;
        _duplicateTarget[0] = mainBuildingTransform;
        targetHealth[0] = mainBuilding.healthManager;
    }


}
