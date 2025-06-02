using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> acriveEnemy;
    public int numberOfEnemies;

    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }


    public bool DayStart()
    {
        if (numberOfEnemies > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }

}
