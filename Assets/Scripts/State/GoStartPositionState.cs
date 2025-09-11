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

        if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) >= _bot._distanceToTarget)
        {
            _bot._animatorController.run = true;//Animator
            _bot._agent.destination = _bot.target[0].transform.position;
            _bot._agent.isStopped = false;
        }
        else
        {
            _bot._agent.isStopped = true;
            _bot._animatorController.run = false;//Animator
        }
        if (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) <= _bot._distanceToTarget
            && _bot.target.Count > 1 || _bot.target.Count > 1)
        {
            _bot.SwitchState(new PatrolState());
        }
    }
    public void Exit(){}
}
