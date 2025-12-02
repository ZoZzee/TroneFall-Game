using UnityEngine;

public class GoStartPositionState : IEnemyState
{

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
    }

    public void FixedUpdate()
    {

        if (_bot.builds.Count > 0)
        {
            _bot.SwitchState(new PatrolState());
        }

        if (Vector3.Distance(_bot.transform.position, _bot._targetPoint.transform.position) > _bot._distanceToTarget)
        {
            _bot._animatorController.run = true;//Animator
            _bot._agent.destination = _bot._targetPoint.transform.position;
            _bot._agent.isStopped = false;
        }
        else
        {
            _bot._agent.isStopped = true;
            _bot._animatorController.run = false;//Animator
        }
    }
    public void Exit(){}
}
