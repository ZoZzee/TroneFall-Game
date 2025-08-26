using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : MonoBehaviour
{
    private IEnemyState _currentState;

    [Header("Status")]
    public bool _itsEnemy;
    public bool _itsAllies;

    [Header("Settings")]
    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public float _speed;                                //+
    public float attackCooldown;                        //+
    public int _damage;                                 //+

    [HideInInspector] public float distanceToTarget;    //+
    public float _distanceToTarget = 3f;

    public bool canAttack;                              //+
    public NavMeshAgent _agent;

    [HideInInspector] public Vector3 spawnPoint;        //+-
    public float rotationSpeed;                         //+
    public float distanseToAttack;                      //+

    [Header("References")]
    public EnemyManager _enemyManager;
    public AlliesManager _alliesManager;
    [HideInInspector] public SpawnScript spawnScript;

    [Header("Components")]
    public AnimatorController _animatorController;

    public GameObject _targetPoint;
    [HideInInspector] public MainBuilding mainBuilding;
    [HideInInspector] public GameObject mainBuildingTransform;

    private void Start()
    {
        if (_itsEnemy)
        {
            mainBuilding = BuildingsManager.instance.mainBuilding;

            _enemyManager = EnemyManager.instance;
            AddTarget();

        }
        else if(_itsAllies)
        {
            _alliesManager = AlliesManager.instance;
            
        }
        _agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        //AddTarget();

        SwitchState(new PatrolState());
        spawnPoint = transform.position;

        
    }
    private void OnDisable()
    {
        if (_itsEnemy)
        {
            _enemyManager.activeEnemy.Remove(this.gameObject);
        }
        else
        {
            _alliesManager.activeAllies.Remove(this.gameObject);
        }
        target.Clear();
        targetHealth.Clear();
    }

    private void FixedUpdate()
    {
        _currentState?.FixedUpdate();
    }
    public void SwitchState(IEnemyState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter(this);
    }


    public void AddTarget()
    {
        if (target.Count == 0 && !target.Contains(_targetPoint))
        {
            if (_itsEnemy)
            {
                mainBuildingTransform = mainBuilding.gameObject;
                if (!target.Contains(mainBuildingTransform))
                {
                    target.Add(mainBuildingTransform);
                    targetHealth.Add(mainBuilding.healthManager);
                }
                _enemyManager.activeEnemy.Add(this.gameObject);
            }
            else if (_itsAllies)
            {
                target.Add(_targetPoint);
                Debug.Log("додав таргет");
                _alliesManager.activeAllies.Add(this.gameObject);
            }
            SwitchState(new PatrolState());
            spawnPoint = transform.position;
        }
    }
}
