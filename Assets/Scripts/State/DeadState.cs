using UnityEngine;

public class DeadState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;

        _bot._agent.isStopped = true;
        _bot._animatorController.run = false;
        _bot._animatorController.dead = true;
    }

    public void FixedUpdate(){}
    public void Exit()
    {
    }
}

