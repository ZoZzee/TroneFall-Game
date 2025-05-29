using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyController> acriveEnemy;

    public EnemyManager enemyManager;

    private void Awake()
    {
        enemyManager = this;
    }


}
