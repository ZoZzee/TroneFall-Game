using NUnit.Framework;
using System.Collections.Generic;
using Unity.AI.Navigation.Samples;
using UnityEngine;

public class AttackState : IEnemyState
{

    [Header("Timer ")]
    private float _time = 0;
    private float halfMeter = 0.5f;
    private int deltaTime = 50;

    private Bot _bot;


    private HealthManager targetHelth;
    private GameObject target;

    public void Enter(Bot bot)
    {
        Debug.Log("Стан атаки");
        _bot = bot;
        _time = _bot.attackCooldown / 2;
        _bot._agent.isStopped = true;
        _bot._animatorController.run = false;
        _bot._agent.ResetPath();
        _bot._agent.SetDestination(_bot.transform.position);

    }

    public void FixedUpdate()
    {
        if (_bot._animatorController.dead == true)
        {
            _bot.SwitchState(new DeadState());
        }
        if(_bot.attackCooldown <= _time)
        {
            if (_bot._itsAllies)
            {
                AlliesAttack();
            }
            else if (_bot._itsEnemy)
            {
                if (_bot.mainBuildingTransform == null)
                {
                    _bot.SwitchState(new VictoryState());
                }
                else
                {
                    EnemyAttack();
                }
            }
        }
        else
        {
            _bot._animatorController.attack = false;
            _time+= Time.deltaTime * deltaTime;
        }
        
    }

    private void AlliesAttack()
    {
        if(_bot.builds.Count == 0)
        {
            NewPatrolState();
            return;
        }
        Target(_bot.builds, _bot.buildsHealth);
        if (_bot.canAttack || Vector3.Distance(_bot.transform.position, target.transform.position) <= _bot.distanseToAttack + halfMeter)
        {
            NewAttack(target, targetHelth);
        }
        else
        {
            NewPatrolState();
        }
    }
    private void EnemyAttack()
    {
        if (_bot.wals.Count > 0)
        {
            Target(_bot.wals, _bot.walsHealth);
            if (_bot.canAttack || Vector3.Distance(_bot.transform.position, target.transform.position) <= _bot.distanseToAttack + halfMeter)
            {
                NewAttack(target, targetHelth);
            }
            else
            {
                NewPatrolState();
            }
        }
        else if (_bot.wals.Count <= 0 && _bot.builds.Count > 0)
        {
            Target(_bot.builds,_bot.buildsHealth);
            if (_bot.canAttack || Vector3.Distance(_bot.transform.position, target.transform.position) <= _bot.distanseToAttack + halfMeter)
            {
                NewAttack(target, targetHelth);
            }
            else
            {
                NewPatrolState();
            }
        }
        else if (_bot.canAttack||
             Vector3.Distance(_bot.transform.position, _bot.mainBuildingTransform.transform.position) <= _bot.distanseToAttack + halfMeter * 3)
        {
            NewAttack(_bot.mainBuildingTransform, _bot.mainBuildingHealth);
        }
        else
        {
            NewPatrolState();
        }
    }

    private void Target(List<GameObject> list, List<HealthManager> healthList)
    {
        GameObject nextTarger;
        target = list[0];
        targetHelth = healthList[0];
        for (int i = 0; i < list.Count; i++)
        {
            nextTarger = list[i];
            
            if (Vector3.Distance( _bot.transform.position,nextTarger.transform.position) < Vector3.Distance(_bot.transform.position,target.transform.position))
            {
                
                target = list[i];
                targetHelth = healthList[i];
            }
        }
    }

    public void Exit()
    {
        _bot.canAttack = false;
        _bot._agent.isStopped = false;
    }

    private void NewPatrolState()
    {
        _bot.SwitchState(new PatrolState());
    }

    private void NewAttack(GameObject target, HealthManager targetHealth)
    {
        _bot.transform.LookAt(target.transform);
        SoundsManager.instance.audioDistance = _bot.audioDistance;
        SoundsManager.instance.PlaySound(_bot.attack, _bot.transform.position);
        _bot._animatorController.attack = true;
        _time = 0;
        targetHealth.MinusHp(_bot._damage);
        if (targetHealth._health <= 0)
        {
            if (_bot._itsEnemy)
            {
                _bot._enemyManager.ClineDeadTarget(target, targetHealth);
                chekDist();
            }
            else if (_bot._itsAllies)
            {
                _bot._alliesManager.ClineDeadTarget(target, targetHealth);
                chekDist();
            }
        }
    }
   
    private void chekDist()
    {
        if (_bot.builds.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) > _bot.distanseToAttack)
        {
            _bot.canAttack = false;
        }
        else
        {
            NewPatrolState();
        }
    }
}