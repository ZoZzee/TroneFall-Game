using UnityEngine;

public class TargetState : IEnemyState
{
    private Bot _bot;
    public void Enter(Bot bot)
    {
        _bot = bot;
        GameObject target = null;
        float distanseBifor = 0;

        for (int i = 0; i < _bot._alliesManager.activeAllies.Count; i++)
        {
            Vector3 enemy = _bot._alliesManager.activeAllies[i].transform.position;
            Vector3 myPosition = _bot.transform.position;
            float distanse = Vector3.Distance(myPosition, enemy);
            if (i > 0 && distanseBifor > distanse)
            {
                target = _bot._alliesManager.activeAllies[i];
            }
            distanseBifor = distanse;
        }
        if(target == null && _bot._itsEnemy)
        {
            _bot.mainBuildingTransform = _bot.mainBuilding.gameObject;
            _bot.target.Add(_bot.mainBuildingTransform);
            _bot.targetHealth.Add(_bot.mainBuilding.healthManager);

        }
        _bot.target.Add(target);
        _bot.targetHealth.Add(target.GetComponent<HealthManager>());
        
    }

    public void FixedUpdate() { }
    public void Exit()
    {
    }
}
