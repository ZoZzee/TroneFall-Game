using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public SpawnPointData spawnPointData;

    private EnemyManager enemyManager;
    [SerializeField] private GameObject _archer;
    [SerializeField] private GameObject _barbarian;

    [HideInInspector] public bool wavefinich = false;
    private void Start()
    {
        enemyManager = EnemyManager.instance;
    }
    public void SpawnEnemys(int currentWave)
    {
        StartCoroutine(SpawnTimer(currentWave));
    }

    private IEnumerator SpawnTimer(int currentWave)
    {
        for (int i = 0; i < spawnPointData.waves[currentWave].enemys.Length; i++)
        {
            for (int j = 0; j < spawnPointData.waves[currentWave].enemys[i].enemy.Length; j++)
            {
                if (spawnPointData.waves[currentWave].enemys[i].enemy[j] == _archer)
                {
                    enemyManager.GetArchers(RandomRange(), Quaternion.identity);
                }
                else if (spawnPointData.waves[currentWave].enemys[i].enemy[j] == _barbarian)
                {
                    enemyManager.GetBarbarian(RandomRange(), Quaternion.identity);
                }
            }

                
            yield return new WaitForSeconds(spawnPointData.waves[currentWave].cooldown);
        }
        wavefinich = true;
    }

    private Vector3 RandomRange()
    {
        Vector3 vectorinia = transform.position;

        return new Vector3(0, 0, 0);
    }
}

[Serializable]
public class SpawnPointData
{
    public Wave[] waves;
}

[Serializable]
public class Wave
{
    public Pack[] enemys;
    public int cooldown;
}
[Serializable]
public class Pack
{
    public GameObject[] enemy;
}