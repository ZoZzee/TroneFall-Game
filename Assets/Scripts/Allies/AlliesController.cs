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
    [SerializeField] private float _maxDistanceToAttack;
    private float _distanceToAttack;
    private float _targetEmptyDistance = 3f;
    [HideInInspector] public float distanceToTarget;
    [HideInInspector] public bool attack = false;
    public List<GameObject> target;
    public List<HealthManager> healthManagers;


    [Header("Components")]
    [SerializeField] private GameObject _targetEmpty;
    public Transform _targetPoint;
    public Transform _lastTargetPoint;
    public AnimatorController _animatorController;
    private void Start()
    {
        _distanceToAttack = _maxDistanceToAttack;
        Vector3 position = spawnScript._pointsPosition[spawnScript.num].position;
        if(spawnScript.deadPosition != null)
        {
            Debug.Log("Помер");
            position = spawnScript.deadPosition.position;
            spawnScript.deadPosition = null;
        }
        GameObject newtargetDot = Instantiate(_targetEmpty, position, Quaternion.identity,null);
        spawnScript.num++;
        _targetPoint = newtargetDot.transform;
        target.Add(_targetPoint.gameObject);
        
        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
        
    }

    private void FixedUpdate()
    {
        if (target[0] != _targetPoint)
        {
            if (_distanceToAttack != _maxDistanceToAttack)
            {
                _maxDistanceToAttack = _distanceToAttack;
            }
            Target();
        }
        else
        {
            if (_distanceToAttack == _maxDistanceToAttack)
            {
                _maxDistanceToAttack = _targetEmptyDistance;
            }
            Target();

        }
    }

    private void Target()
    {
        transform.LookAt(target[0].transform);
        if (!target[0].activeInHierarchy)
        {
            Debug.Log("Очистка");
            RefreshTarget();
        }
        if (!_animatorController.dead && distanceToTarget > _maxDistanceToAttack)
        {
            attack = false;
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].transform.position, _walkingSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            _animatorController.velocity = smoothedPosition.normalized.magnitude;
        }
        else if (distanceToTarget <= _maxDistanceToAttack && target[0] != _targetPoint)
        {
            _animatorController.velocity = 0;
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
                    RefreshTarget();
                }
            }
            yield return new WaitForSeconds(0.1f);
            _animatorController.attack = false;
            yield return new WaitForSeconds(attackCooldown - 0.1f);

        }
    }

    private void RefreshTarget()
    {
        healthManagers.RemoveAt(0);
        target.RemoveAt(0);
    }
}
