using System.Collections.Generic;
using UnityEngine;

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
        }
        else if(_itsAllies)
        {
            _alliesManager = AlliesManager.instance;
            AddTarget(_targetPoint);
        }
    }
    private void OnEnable()
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
        else if(_itsAllies)
        {
            Debug.Log(" Target point " + _targetPoint);
            //_targetPoint = target[0];
            AddTarget(_targetPoint);
            _alliesManager.activeAllies.Add(this.gameObject);
        }
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


    public void AddTarget( GameObject _object)
    {

        Debug.Log(" Target point " + _targetPoint);
        if (_object != null)
        {
            target.Add(_object);
        }
    }
}
