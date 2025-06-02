using UnityEngine;

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
        if (other.CompareTag("Player"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    enemyController.target = other.transform;
                    enemyController.targetHealth = other.GetComponent<HealthManager>();
                    break;
                case TriggerPriority.priority1:
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
                case TriggerPriority.priority2:
                    enemyController.SetMainBuildingAsTarget();
                    break;
            }   
        }
    }
}
