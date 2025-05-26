using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [SerializeField] private EnemyController enemyController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyController.target = other.transform;
            enemyController.targetHealth = other.GetComponent<HealthManager>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemyController.SetMainBuildingAsTarget();
        }
    }
}
