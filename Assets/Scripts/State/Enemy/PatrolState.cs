using UnityEngine;

public class PatrolState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
    }

    public void FixedUpdate()
    {
        _bot.transform.position = Vector3.MoveTowards(_bot.transform.position, _bot.target.position, _bot.speed);
        if(Vector3.Distance(_bot.transform.position, _bot.target.position) < _bot.distanseToAttack)
        {
            //Idle
        }
    }
    public void Exit()
    {

    }
}
