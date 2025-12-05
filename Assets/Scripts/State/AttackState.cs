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
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) <= _bot.distanseToAttack + halfMeter)
                {
                    NewAttack(_bot.builds[0], _bot.buildsHealth[0]);
                }
                else
                {
                    NewPatrolState();
                }
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

    private void EnemyAttack()
    {
        if (_bot.wals.Count > 0)
        {
            if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.wals[0].transform.position) <= _bot.distanseToAttack + halfMeter)
            {
                NewAttack(_bot.wals[0], _bot.walsHealth[0]);
            }
            else
            {
                NewPatrolState();
            }
        }
        else if (_bot.builds.Count > 0)
        {
            GameObject priorityTarger;
            target = _bot.builds[0];
            targetHelth = _bot.buildsHealth[0];
            for (int i = 0; i < _bot.builds.Count; i++)
            {
                priorityTarger = _bot.builds[i];
                if (_bot.canAttack || Vector3.Distance(priorityTarger.transform.position, _bot.transform.position) < Vector3.Distance(target.transform.position, _bot.transform.position))
                {
                    target = _bot.builds[i];
                    targetHelth = _bot.buildsHealth[i];
                }
            }
            if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) <= _bot.distanseToAttack + halfMeter)
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

    public void Exit()
    {
        Debug.Log("Вийшов з атаки");
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