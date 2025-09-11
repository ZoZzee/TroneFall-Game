using UnityEngine;

public class AttackState : IEnemyState
{
    private float timer = 0f;

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
        timer = _bot.attackCooldown / 2f;
    }

    public void FixedUpdate()
    {
        if (_bot.target.Count > 0)
        {
            _bot.transform.LookAt(_bot.target[0].transform);

            if ( _bot.canAttack && Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) <= _bot.distanseToAttack)
            {
                _bot._animatorController.run = false;
                _bot._agent.isStopped = true;


                if (timer >= _bot.attackCooldown)
                {
                    if(_bot.target[0] == _bot._targetPoint)
                    {
                        Exit();
                    }

                    _bot._animatorController.attack = true;
                    timer = 0f;
                    if (_bot.targetHealth[0].CheckHP(_bot._damage) <= 0)
                    {
                        if (_bot._itsEnemy)
                        {
                            _bot._enemyManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                            Debug.Log("Refresh");
                        }
                        else if (_bot._itsAllies)
                        {
                            _bot._alliesManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                            Debug.Log("Refresh");
                        }
                    }
                    Debug.Log("Дамаг = " + _bot._damage);
                    _bot.targetHealth[0].MinusHp(_bot._damage);
                }
                else
                {
                    _bot._agent.isStopped = false;
                    _bot._animatorController.attack = false;
                    
                    _bot._animatorController.attack = false;
                    timer++;
                }
            }
            else
            {

                _bot._animatorController.run = true;
                _bot._agent.isStopped = false;
                _bot._agent.destination = _bot.target[0].transform.position;
            }
        }
        else
        {
            Exit();
        }


    }
    public void Exit()
    {

        _bot.canAttack = false;
        _bot.SwitchState(new PatrolState());
    }


    
}
