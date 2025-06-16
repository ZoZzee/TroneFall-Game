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
                        enemyController.target[0] = other.transform;
                        enemyController.targetHealth[0] = other.GetComponent<HealthManager>();
                    }
                    else
                    {
                        enemyController.target.Add(other.transform);
                        enemyController.targetHealth.Add(other.GetComponent<HealthManager>());
                    }
                    
                break;
                case TriggerPriority.priority2:
                break;
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") ||
            other.CompareTag("PlayerAllies") ||
            other.CompareTag("Buldings"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    
                    enemyController.enemyAttack = false;
                    break;
                case TriggerPriority.priority2:

                    enemyController.target.Remove(other.transform);
                    enemyController.targetHealth.Remove(other.GetComponent<HealthManager>());
                    if(enemyController.target == null)
                    {
                        enemyController.SetMainBuildingAsTarget();
                    }
                    Debug.Log("видалдив Target з Enemy");

                    enemyController.SetMainBuildingAsTarget();
                    break;
            }   
        }
        
    }
}
