using UnityEngine;

public class DeadState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;

        _bot._agent.isStopped = true;
        _bot._animatorController.dead = true;
        if (_bot._itsAllies)
        {
            _bot.spawnScript._dedAlliesSpawnPoint.Add(_bot._targetPoint);
            _bot.spawnScript.myAllies.Remove(_bot.gameObject);
        }
    }

    public void FixedUpdate(){}
    public void Exit()
    {
    }
}

