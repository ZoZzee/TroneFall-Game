using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
    }

    public void FixedUpdate()
    {
        if (_bot._animatorController.dead == true)
        {
            _bot.SwitchState(new DeadState());
        }

        if (_bot._itsAllies)
        {
            if (_bot.target.Count > 1)
            {
                
                _bot.transform.LookAt(_bot.target[0].transform);
                _bot.SetDestination(_bot.target[0].transform);
                _bot._animatorController.run = true;//Animator
                if (_bot.canAttack && _bot.target[0] != _bot._targetPoint ||
                    Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot.distanseToAttack &&
                    _bot.target[0] != _bot._targetPoint)
                {
                    _bot.SwitchState(new AttackState());
                }
                else if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot._distanceToTarget)
                {
                    _bot._animatorController.run = false;        // ÀAnimator
                    return;
                }
            }
            else
            {
                _bot.SwitchState(new GoStartPositionState());
            }

        }


        else if (_bot._itsEnemy)
        {
            if (_bot.target.Count > 0)
            {
                _bot.transform.LookAt(_bot.target[0].transform);
                _bot.SetDestination(_bot.target[0].transform);
                _bot._animatorController.run = true;
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) <= _bot.distanseToAttack)
                {
                    _bot.SwitchState(new AttackState());
                    
                }
            }
            else
            {
                return;
            }
        }
        
    }
    public void Exit(){}
}
