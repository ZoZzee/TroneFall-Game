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

    [Header("Targets")]
    public List<GameObject> builds;
    public List<GameObject> wals;
    public List<HealthManager> buildsHealth;
    public List<HealthManager> walsHealth;

    public GameObject _targetPoint;


    [Header("Settings")]
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
    public BuildingsManager mainBuilding;
    public GameObject mainBuildingTransform;
    public HealthManager mainBuildingHealth;
    public EnemyManager _enemyManager;
    public EnemyTrigger p_enemyTrigger;

    [Header("Allies components")]
    [HideInInspector] public SpawnScript spawnScript;
    public AlliesManager _alliesManager;
    public AlliesTrigger p_alliesTrigger;

    [Header("Components")]
    public AnimatorController _animatorController;
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
        builds.Clear();
        wals.Clear();
        buildsHealth.Clear();
        walsHealth.Clear();
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
        if (_itsEnemy)
        {
            mainBuildingTransform = mainBuilding.mainBuilding.gameObject;
            mainBuildingHealth = mainBuilding.mainBuilding.GetComponent<HealthManager>();
        }
        SwitchState(new PatrolState());
        spawnPoint = transform.position;
    }

    public void SetDestination(Transform target)
    {
        _agent.SetDestination(target.position);
    }
}
