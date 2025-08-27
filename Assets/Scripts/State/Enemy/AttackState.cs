using UnityEngine;

public class AttackState : IEnemyState
{
    private float timer = 0f;

    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
        timer = _bot.attackCooldown / 2f;
    }

    public void FixedUpdate()
    {
        
        if (_bot.target.Count > 0 
            && (Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) < _bot.distanseToAttack
            || _bot.canAttack))
        {
            _bot.transform.LookAt(_bot.target[0].transform);
            _bot._animatorController.velocity = 0;
            if (timer >= _bot.attackCooldown)
            {
                _bot._animatorController.attack = true;
                timer = 0f;
                if (_bot.targetHealth[0].CheckHP( _bot._damage) == 0)
                {
                    if (_bot._itsEnemy)
                    {
                        for (int i = 0; i < _bot._enemyManager.activeEnemy.Count; i++)
                        {
                            _bot._enemyManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                            Debug.Log("Refresh");

                        }
                    }
                    else if (_bot._itsAllies)
                    {
                        for (int i = 0; i < _bot._alliesManager.activeAllies.Count; i++)
                        {

                            _bot._alliesManager.ClineDeadTarget(_bot.target[0], _bot.targetHealth[0]);
                            Debug.Log("Refresh");

                        }
                    }
                }

                _bot.targetHealth[0].MinusHp(_bot._damage);
            }
            else
            {
                _bot._animatorController.attack = false;
                timer++;
            }


        }
        else
        {
            Debug.Log("Атакує з  +  " + Vector3.Distance(_bot.transform.position, _bot.target[0].transform.position) + " " + _bot.canAttack);
            _bot.canAttack = false;
            _bot.SwitchState(new PatrolState());
        }


    }
    public void Exit(){}


    
}
