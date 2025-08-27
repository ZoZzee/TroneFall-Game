using System;
using System.Collections;
using UnityEditor.Searcher;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public SpawnPointData spawnPointData;

    private EnemyManager enemyManager;
    [SerializeField] private GameObject _archer;
    [SerializeField] private GameObject _barbarian;

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

            if(spawnPointData.waves[currentWave].enemys[i] == _archer)
            {
                enemyManager.GetArchers(transform.position, Quaternion.identity);


            }
            else if(spawnPointData.waves[currentWave].enemys[i] == _barbarian)
            {
                enemyManager.GetBarbarian(transform.position, Quaternion.identity);
            }

                //Instantiate(spawnPointData.waves[currentWave].enemys[i], transform.position, Quaternion.identity, null);
            yield return new WaitForSeconds(spawnPointData.waves[currentWave].cooldown);
        }
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
    public GameObject[] enemys;
    public int cooldown;
}
