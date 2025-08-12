using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private int _currentWave;
    [SerializeField] private SpawnPoint[] spawnPoints;

    public EnemyFactory factory;

    public static SpawnManager instance;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Enemy Archer = factory.CreateEnemy(EnemyType.EnemyTypes.Archer, new Vector3(10,0,3));
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
            spawnPoints[i].SpawnEnemys(_currentWave);
        }

        _currentWave++;
    }
}
