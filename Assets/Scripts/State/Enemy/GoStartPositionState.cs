using UnityEngine;

public class GoStartPositionState : MonoBehaviour
{

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
    }

    public void FixedUpdate()
    {
        _bot.transform.position = Vector3.MoveTowards(_bot.transform.position, _bot.spawnPoint, _bot.speed);
        if (Vector3.Distance(_bot.transform.position, _bot.spawnPoint) < 1f)
        {
            //Idle
        }
    }
    public void Exit(){}
}
