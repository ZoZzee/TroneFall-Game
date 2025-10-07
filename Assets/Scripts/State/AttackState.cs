using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IEnemyState
{
    private float timer = 0f;

    private Bot _bot;
    public void Enter(Bot bot)
    {
        Debug.Log("Стан атаки");

        _bot = bot;

        timer = _bot.attackCooldown / 2f;

        _bot._agent.isStopped = true;
        _bot._animatorController.run = false;
        _bot._agent.ResetPath();
        _bot._agent.SetDestination(_bot.transform.position);
    }

    public void FixedUpdate()
    {
        if(_bot._animatorController.dead == true)
        {
            _bot.SwitchState(new DeadState());
        }

        if (_bot.target.Count >= 1 && _bot.target[0] != _bot._targetPoint)
        {
            _bot.transform.LookAt(_bot.target[0].transform);

            if ( _bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) <= _bot.distanseToAttack)
            {
                if (timer >= _bot.attackCooldown)
                {

                    SoundsManager.instance.PlaySound(_bot.attack, _bot.transform.position);
                    _bot._animatorController.attack = true;
                    _bot.targetHealth[0].MinusHp(_bot._damage);
                    timer = 0f;
                    if (_bot.targetHealth[0]._health<= 0)
                    {
                        if (_bot._itsEnemy)
                        {
                            _bot._enemyManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                        }
                        else if (_bot._itsAllies)
                        {
                            _bot._alliesManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                        }
                    }
                }
                else
                {
                    _bot._animatorController.attack = false;
                    timer++;
                }
            }
            else
            {
                _bot.SwitchState(new PatrolState());
            }
        }
        else
        {
            _bot.SwitchState(new PatrolState());
        }
    }
    public void Exit()
    {
        Debug.Log("Exid()");
        _bot.canAttack = false;
        _bot._agent.isStopped = false;
    }
}
