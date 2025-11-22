//using UnityEngine;
//using static EnemyType;

//public class EnemyFactory : MonoBehaviour
//{
//    public GameObject archerPrefab;
//    public GameObject barbarianPrefab;

//    public EnemyFactory instanse;
//    private void Awake()
//    {
//        instanse = this;
//    }

//    public Enemy CreateEnemy(EnemyType.EnemyTypes type, Vector3 position)
//    {
//        GameObject enemyGo = null;
//        switch(type)
//        {
//            case EnemyType.EnemyTypes.Archer:
//                enemyGo = Instantiate(archerPrefab,position,Quaternion.identity);
//                break;
//            case EnemyType.EnemyTypes.Barbarian:
//                enemyGo = Instantiate(archerPrefab, position, Quaternion.identity);
//                break;
//        }
//        return enemyGo.GetComponent<Enemy>();
//    }
//}
