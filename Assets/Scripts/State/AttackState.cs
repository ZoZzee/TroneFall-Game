using UnityEngine;

public class AttackState : IEnemyState
{

    [Header("Timer ")]
    private float _time = 0;
    private float halfMeter = 0.5f;
    private int deltaTime = 50;

    private Bot _bot;
    public void Enter(Bot bot)
    {
        Debug.Log("Стан атаки");

        _bot = bot;
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
        if(_bot.builds.Count == 0
            ||((_bot.wals.Count <= 0 && _bot.builds.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) > _bot.distanseToAttack + halfMeter)
            ||(_bot.wals.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.wals[0].transform.position) > _bot.distanseToAttack + halfMeter)))
        {
            NewPatrolState();
        }
        if(_bot.wals.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.wals[0].transform.position) > _bot.distanseToAttack + halfMeter)
        {

        }
        if(_bot.attackCooldown <= _time)
        {
            _bot.transform.LookAt(_bot.builds[0].transform);
            
            if (_bot._itsAllies)
            {
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) <= _bot.distanseToAttack + halfMeter)
                {
                    NewAttack(_bot.builds[0], _bot.buildsHealth[0]);
                }
            }
            else if (_bot._itsEnemy)
            {
                if (_bot.wals.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.wals[0].transform.position) <= _bot.distanseToAttack + halfMeter)
                {
                    Debug.Log("2");
                    NewAttack(_bot.wals[0], _bot.walsHealth[0]);
                }
                else if (_bot.builds.Count > 0 || Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) <= _bot.distanseToAttack + halfMeter)
                {
                    Debug.Log("Build triger");
                    NewAttack(_bot.builds[0], _bot.buildsHealth[0]);
                }
            }
        }
        else
        {
            _bot._animatorController.attack = false;
            _time+= Time.deltaTime * deltaTime;
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
        if (Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) > _bot.distanseToAttack)
        {
            _bot.canAttack = false;
        }
    }
}