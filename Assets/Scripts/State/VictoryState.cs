using UnityEngine;

public class VictoryState : IEnemyState
{

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;

        _bot._agent.isStopped = true;
        _bot._animatorController.attack = false;
        _bot._animatorController.dead = false;
        _bot._animatorController.run = false;

    }

    public void FixedUpdate() { }
    public void Exit()
    {
    }
}



