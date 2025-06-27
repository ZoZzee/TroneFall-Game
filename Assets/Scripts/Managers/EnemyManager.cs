using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> activeEnemy;
    public int numberOfEnemies;

    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }


    public bool DayStart()
    {
        if (activeEnemy.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

}
