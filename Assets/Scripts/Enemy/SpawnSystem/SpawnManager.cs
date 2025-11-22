using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int currentWave;
    public SpawnPoint[] spawnPoints;


    public static SpawnManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
                Destroy(GameObject.FindGameObjectsWithTag("Enemy")[i].gameObject);
            }
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].SpawnEnemys(currentWave);
        }

        currentWave++;
    }
}
