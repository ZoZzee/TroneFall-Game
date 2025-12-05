using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PatrolState : IEnemyState
{
    private Bot _bot;


    private HealthManager targetHelth;
    private GameObject target;
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
            if (_bot.builds.Count > 0)
            {
                _bot.transform.LookAt(_bot.builds[0].transform);
                _bot.SetDestination(_bot.builds[0].transform);
                _bot._animatorController.run = true;//Animator
                if (_bot.canAttack || Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) < _bot.distanseToAttack)
                {
                    _bot.SwitchState(new AttackState());
                }
                else if (Vector3.Distance(_bot.transform.position, _bot.builds[0].transform.position) < _bot._distanceToTarget)
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
            if (_bot.mainBuildingTransform != null)
            {
                target = _bot.mainBuildingTransform;
                targetHelth = _bot.mainBuildingHealth;
                if (_bot.builds.Count > 0)
                {
                    GameObject priorityTarger;
                    for(int i = 0; i< _bot.builds.Count; i++)
                    {
                        priorityTarger = _bot.builds[i];
                        if (Vector3.Distance(priorityTarger.transform.position, _bot.transform.position) < Vector3.Distance(target.transform.position, _bot.transform.position))
                        {
                            target = _bot.builds[i];
                            targetHelth = _bot.buildsHealth[i];
                        }
                    }
                }
                _bot.transform.LookAt(target.transform);
                _bot.SetDestination(targetHelth.transform);
                _bot._animatorController.run = true;
                if (_bot.canAttack ||
                    Vector3.Distance(_bot.transform.position, target.transform.position) < _bot.distanseToAttack ||
                    _bot.wals.Count > 0 && Vector3.Distance(_bot.transform.position, _bot.wals[0].transform.position) < _bot.distanseToAttack)
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
