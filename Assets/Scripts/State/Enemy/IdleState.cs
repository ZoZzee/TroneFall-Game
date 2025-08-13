using UnityEngine;

public class IdleState : IEnemyState
{
    private float max_Time = 150f;
    private float timer = 0f;

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
    }

    public void FixedUpdate()
    {
        if(_bot.target != null)
        {
            if(Vector3.Distance(_bot.transform.position, _bot.target.position) < _bot.distanseToAttack)
            {
                if(timer!=max_Time)
                {
                    Debug.Log("Бот ударив");

                    timer = 0f;
                }else
                {
                    timer++;
                }
            }
        }
        else
        {
            _bot.SwitchState(new PatrolState());
        }
        
    }
    public void Exit(){}
}
