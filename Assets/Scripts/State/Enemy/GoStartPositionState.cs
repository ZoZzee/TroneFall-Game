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
        if (_bot._itsEnemy)
        {
            _bot.transform.position = Vector3.MoveTowards(_bot.transform.position, _bot.spawnPoint, _bot._speed);
            if (Vector3.Distance(_bot.transform.position, _bot.spawnPoint) < 1f 
                && _bot.target.Count > 0 || _bot.target.Count > 0)
            {
                _bot.SwitchState(new PatrolState());
            }
        }
        else if (_bot._itsAllies)
        {
            _bot.transform.position = Vector3.MoveTowards(_bot.transform.position, _bot.spawnPoint, _bot._speed);
            if (Vector3.Distance(_bot.transform.position, _bot.spawnPoint) < 1f
                && _bot.target.Count > 0 || _bot.target.Count > 0)
            {
                _bot.SwitchState(new PatrolState());
            }
        }
    }
    public void Exit(){}
}
