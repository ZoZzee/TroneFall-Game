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
        //
        //Debug.Log("i have target");
        if (_bot._itsAllies ||
            !_bot._animatorController.dead &&
            //Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) > _bot.distanseToAttack ||
            _bot.target.Count > 1)
        {
            Debug.Log(" Дистанція між союзником і ворогом " + (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position)));
            _bot.transform.LookAt(_bot.target[0].transform);
            if (_bot.canAttack && _bot.target[0] != _bot._targetPoint ||
                Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot.distanseToAttack &&
                _bot.target[0] != _bot._targetPoint)
            {
                _bot._agent.isStopped = true;
                _bot.SwitchState(new AttackState());
            }
            if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) > _bot.distanceToTarget)
            {
                //Vector3 smoothedPosition = Vector3.MoveTowards(_bot.transform.position, _bot.target[0].transform.position, _bot._speed);
                //_bot.transform.position = smoothedPosition;
                _bot._agent.destination = _bot.target[0].transform.position;
                _bot._animatorController.velocity = _bot._agent.speed;//Аніматор
            }
            else if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) > _bot._distanceToTarget)
            {
                _bot._animatorController.velocity = 0;                                          // Аніматор
                _bot.SwitchState(new GoStartPositionState());

            }

        }
        else if (_bot._itsEnemy)
        {
            if (_bot.target.Count > 0 && !_bot._animatorController.dead)
            {
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot.distanseToAttack)
                {
                    _bot.SwitchState(new AttackState());
                }
                _bot.transform.LookAt(_bot.target[0].transform);
                Vector3 smoothedPosition = Vector3.MoveTowards(_bot.transform.position, _bot.target[0].transform.position, _bot._speed);
                _bot.transform.position = smoothedPosition;
                _bot._animatorController.velocity = smoothedPosition.normalized.magnitude;
            }
            else if (_bot.target.Count < 1 && !_bot._animatorController.dead)
            {
                _bot.SwitchState(new GoStartPositionState());
            }
        }
        
    }
    public void Exit()
    {

    }
}
