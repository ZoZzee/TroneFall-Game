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
    [HideInInspector] public float distanceToTarget;
    [HideInInspector] public bool attack = false;
    public List<Transform> target;
    public List<HealthManager> healthManagers;


    [Header("Components")]
    public Transform _targetPoint;
    public AnimatorController _animatorController;
    private void Start()
    {
        target.Add(_targetPoint);
        StartCoroutine(CheckDistance());
        StartCoroutine(AttackTimer());
    }

    private void FixedUpdate()
    {
        if (target.Count > 0  && target[0] != null)
        {
            Target();
        }
        else if(target.Count < 1 && target[0] == null)
        {
            target.Add(_targetPoint);
            Debug.Log(target[0]);
        }
    }

    private void Target()
    {
        Debug.Log(distanceToTarget);
        transform.LookAt(target[0]);
        if (!_animatorController.dead && distanceToTarget > _maxDistanceToAttack)
        {
            attack = false;
            Vector3 smoothedPosition = Vector3.MoveTowards(transform.position, target[0].position, _walkingSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
            _animatorController.velocity = smoothedPosition.normalized.magnitude;
        }
        else if (distanceToTarget <= _maxDistanceToAttack && target[0] != _targetPoint)
        {
            _animatorController.velocity = 0;
            attack = true;
        }
    }
    


    private IEnumerator CheckDistance()
    {
        yield return new WaitForSeconds(0.1f);
        while (true && !_animatorController.dead)
        {
            if (target.Count > 0)
            {
                distanceToTarget = Vector3.Distance(target[0].position, transform.position);

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
                    Debug.Log("Знищення тавера");
                    healthManagers.RemoveAt(0);
                    target.RemoveAt(0);
                }
            }
            yield return new WaitForSeconds(0.1f);
            _animatorController.attack = false;
            yield return new WaitForSeconds(attackCooldown - 0.1f);

        }
    }
}
