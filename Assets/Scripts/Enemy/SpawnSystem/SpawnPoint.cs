using System;
using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public SpawnPointData spawnPointData;

    public void SpawnEnemys(int currentWave)
    {
        StartCoroutine(SpawnTimer(currentWave));
    }

    private IEnumerator SpawnTimer(int currentWave)
    {
        for (int i = 0; i < spawnPointData.waves[currentWave].enemys.Length; i++)
        {
            Instantiate(spawnPointData.waves[currentWave].enemys[i], transform.position, Quaternion.identity, null);
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
