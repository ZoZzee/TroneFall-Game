using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
       Debug.Log(_bot._distanceToTarget);
    }

    public void FixedUpdate()
    {
        
        if (_bot._itsAllies ||
            !_bot._animatorController.dead)
        {
            if (_bot.target.Count > 1)
            {
                
                _bot.transform.LookAt(_bot.target[0].transform);
                if (_bot.canAttack && _bot.target[0] != _bot._targetPoint ||
                    Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot.distanseToAttack &&
                    _bot.target[0] != _bot._targetPoint)
                {
                    _bot._agent.isStopped = true;
                    _bot.SwitchState(new AttackState());
                }
                else if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) > _bot._distanceToTarget)
                {
                    _bot._agent.destination = _bot.target[0].transform.position;
                    _bot._animatorController.run = true;//Animator
                }
                else if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot._distanceToTarget)
                {
                    _bot._animatorController.run = false;              // ÀAnimator
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
            if (_bot.target.Count > 0 && !_bot._animatorController.dead)
            {
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot._distanceToTarget)
                {
                    _bot._animatorController.run = false;
                    _bot._agent.isStopped = true;
                    _bot.SwitchState(new AttackState());
                }
                _bot.transform.LookAt(_bot.target[0].transform);
                //Vector3 smoothedPosition = Vector3.MoveTowards(_bot.transform.position, _bot.target[0].transform.position, _bot._speed);
                //_bot.transform.position = smoothedPosition;
                //_bot._animatorController.velocity = smoothedPosition.normalized.magnitude;
                Debug.Log(" Дистанція до таргету " + (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position)));
                _bot._agent.destination = _bot.target[0].transform.position;
                _bot._animatorController.run = true;//Animator
            }
            else 
            {
                return;
            }
        }
        
    }
    public void Exit(){}
}
