using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> target;
    public List<HealthManager> targetHealth;

    [SerializeField] private float _speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damageToPlayer;

     public MainBuilding mainBuilding;
    [HideInInspector] public GameObject mainBuildingTransform;

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
        mainBuildingTransform = mainBuilding.gameObject;

        SetMainBuildingAsTarget();

        _enemyManager = EnemyManager.instance;
    }

    private void OnEnable()
    {

        _enemyManager.activeEnemy.Add(this.gameObject);
    }
    

    private void Update()
    {
        if (!target[0].activeInHierarchy)
        {
            Debug.Log("Очистка");
            RefreshTarget();
        }
        if (!enemyAttack && target.Count > 0 && !_animatorController.dead)
        {
            transform.LookAt(target[0].transform);
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].transform.position, _speed * Time.deltaTime);
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

        Debug.Log("RefreshTarget");
    }

    public void SetMainBuildingAsTarget()
    {
        target.Add(mainBuildingTransform);
        targetHealth.Add(mainBuilding.healthManager);
    }


}
