using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlliesController : MonoBehaviour
{
    [Header("Parameters")]
    [HideInInspector] public SpawnScript spawnScript;
    [SerializeField] private float _walkingSpeed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int _damage;
    [HideInInspector] public bool attack = false;

    [SerializeField] private float _maxDistanceToAttack = 15f;
    private float _distanceToAttack;
    private float _targetEmptyDistance = 3f;
    [HideInInspector] public float distanceToTarget;

    public List<GameObject> target;
    public List<HealthManager> healthManagers;

    [Header("Components")]
    [SerializeField] private GameObject _targetEmpty;
    public Transform _targetPoint;
    public AnimatorController _animatorController;

    private AlliesManager _alliesManager;
    private void Start()
    {
        _alliesManager = AlliesManager.instance;
        _distanceToAttack = _maxDistanceToAttack;
        
    }

    private void OnDisable()
    {
        _alliesManager.activeAllies.Remove(this.gameObject);
        StopAllCoroutines();
    }
    private void OnEnable()
    {
        _alliesManager.activeAllies.Add(this.gameObject);
        Debug.Log("Додано точку");
        //Vector3 position = spawnScript._pointsPosition[spawnScript.num].position;
        //GameObject newtargetDot = _alliesManager.GetPoint(position, Quaternion.identity);
        //spawnScript.num++;
        //_targetPoint = newtargetDot.transform;
        target.Add(_targetPoint.gameObject);
        Debug.Log("Додано точку");

        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
    }

    private void FixedUpdate()
    {
        if (target[0] != _targetPoint)
        {
            if (_distanceToAttack != _maxDistanceToAttack)
            {
                _distanceToAttack = _maxDistanceToAttack;
            }
            Target();
        }
        else
        {
            if (_distanceToAttack == _maxDistanceToAttack)
            {
                _distanceToAttack = _targetEmptyDistance;
            }
            Target();

        }
    }

    private void Target()
    {
        transform.LookAt(target[0].transform);
        
        if (!_animatorController.dead && distanceToTarget > _distanceToAttack)
        {
            attack = false;
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].transform.position, _walkingSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            _animatorController.velocity = smoothedPosition.normalized.magnitude; //Аніматор
        }
        else if (distanceToTarget <= _distanceToAttack && target[0] != _targetPoint)
        {
            _animatorController.velocity = 0;                                    // Аніматор

            attack = true;
        }
        else
        {
            _animatorController.velocity = 0;
        }
    }
    


    private IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(0.1f);
        while (true && !_animatorController.dead)
        {
            if(target.Count > 0)
            {
                distanceToTarget = Vector3.Distance(target[0].transform.position, transform.position);

            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1f);

        while (true && !_animatorController.dead)
        {
            if (attack)
            {
                _animatorController.attack = true;
                healthManagers[0].MinusHp(_damage);
                if (healthManagers[0]._health <= 0f)
                {
                    for (int i = 0; i < _alliesManager.activeAllies.Count; i++)
                    {
                        Debug.Log("Зашло в перевірку союзника");
                        EnemyController enemy = _alliesManager.activeAllies[i].GetComponent<EnemyController>();
                        enemy.target.Remove(target[0]);
                        enemy.targetHealth.Remove(healthManagers[0]);
                    }

                    Debug.Log("Або не зайшло союзника");
                }
            }
            yield return new WaitForSeconds(0.1f);
            _animatorController.attack = false;
            yield return new WaitForSeconds(attackCooldown - 0.1f);

        }
    }
}
