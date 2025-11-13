using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    private IEnemyState _currentState;

    [Header("Switch")]
    public bool _itsEnemy;
    public bool _itsAllies;

    [Header("Settings")]
    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    public float _speed;                                //+
    public float attackCooldown;                        //+
    public int _damage;                                 //+

    public float _distanceToTarget = 3f;

    public bool canAttack;                              //+
    public NavMeshAgent _agent;

    [HideInInspector] public Vector3 spawnPoint;        //-
    public float rotationSpeed;                         //+
    public float distanseToAttack;                      //+


    [Header("Enemy components")]
    [HideInInspector] public BuildingsManager mainBuilding;
    [HideInInspector] public GameObject mainBuildingTransform;
    public EnemyManager _enemyManager;
    public EnemyTrigger p_enemyTrigger;

    [Header("Allies components")]
    [HideInInspector] public SpawnScript spawnScript;
    public AlliesManager _alliesManager;
    public AlliesTrigger p_alliesTrigger;

    [Header("Components")]
    public AnimatorController _animatorController;
    public GameObject _targetPoint;
    public Rigidbody p_rigidbody;


    [Header("Aydio")]
    public AudioClip attack;
    public float audioDistance;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        if (_itsEnemy)
        {
            mainBuilding = BuildingsManager.instance;
            _enemyManager = EnemyManager.instance;
            _enemyManager.activeEnemy.Add(this.gameObject);
            AddTarget();

        }
        else if (_itsAllies)
        {
            _alliesManager = AlliesManager.instance;
            _alliesManager.activeAllies.Add(this.gameObject);
        }
        SwitchState(new PatrolState());
        spawnPoint = transform.position;

    }
    private void OnDisable()
    {
        _animatorController.dead = false;

        if (_itsEnemy)
        {
            if (_enemyManager.activeEnemy.Contains(this.gameObject))
            {
                _enemyManager.activeEnemy.Remove(this.gameObject);
            }
        }
        else
        {
            if (_alliesManager.activeAllies.Contains(this.gameObject))
            {
                _alliesManager.activeAllies.Remove(this.gameObject);
            }
        }
        target.Clear();
        targetHealth.Clear();
    }

    private void FixedUpdate()
    {
        p_rigidbody.linearVelocity = Vector3.zero;
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
        if (target.Count == 0)
        {
            if (_itsEnemy)
            {
                mainBuildingTransform = mainBuilding.mainBuilding.gameObject;
                    targetHealth.Add(mainBuilding.mainBuilding.GetComponent<HealthManager>());
                    target.Add(mainBuildingTransform);
            }
            else if (_itsAllies)
            {
                target.Add(_targetPoint);
            }
            SwitchState(new PatrolState());
            spawnPoint = transform.position;
        }
    }

    public void SetDestination(Transform target)
    {
        _agent.SetDestination(target.position);
    }
}
