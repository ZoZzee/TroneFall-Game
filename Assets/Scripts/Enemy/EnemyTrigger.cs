using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyTrigger : MonoBehaviour
{

    [SerializeField] private EnemyController enemyController;

    public TriggerPriority currentTriggerPriority;

    public enum TriggerPriority
    {
        priority0,
        priority1,
        priority2
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || 
            other.CompareTag("Buldings") || 
            other.CompareTag("PlayerAllies"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    enemyController.enemyAttack = true;
                    break;
                case TriggerPriority.priority1:

                    if (enemyController.target[0] == enemyController.mainBuildingTransform)
                    {
                        Debug.Log("Колізія з гравцем");
                        enemyController.target[0] = other.transform;
                        enemyController.targetHealth[0] = other.GetComponent<HealthManager>();
                    }
                    else
                    {
                        enemyController.target.Add(other.transform);
                        enemyController.targetHealth.Add(other.GetComponent<HealthManager>());
                    }
                    
                    Debug.Log("Зупинився і повернувся на ворога");
                    break;
                case TriggerPriority.priority2:
                    Debug.Log("Побачив ворога");
                    break;
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    enemyController.enemyAttack = false;
                    break;
                case TriggerPriority.priority2:

                    enemyController.target.Remove(other.transform);
                    enemyController.targetHealth.Remove(other.GetComponent<HealthManager>());

                    Debug.Log("видалдив гравця з ворога");

                    enemyController.SetMainBuildingAsTarget();
                    break;
            }   
        }
    }
}
