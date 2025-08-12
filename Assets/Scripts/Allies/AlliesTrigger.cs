using UnityEngine;

public class AlliesTrigger : MonoBehaviour
{
    [SerializeField] private AlliesController _alliesController;

    public TriggerPriority currentTriggerPriority;

    private byte _positionOnList = 0;
    public enum TriggerPriority
    {
        priority0,
        priority1
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                    break;
                case TriggerPriority.priority1:
                    
                    _alliesController.healthManagers.Add(other.GetComponent<HealthManager>());
                    _alliesController.target.Insert(_positionOnList,other.gameObject);
                    _positionOnList++;
                    break;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            switch (currentTriggerPriority)
            {
                case TriggerPriority.priority0:
                   // _alliesController.attack = false;
                    break;
                case TriggerPriority.priority1:

                    _alliesController.healthManagers.Remove(other.GetComponent<HealthManager>());
                    _alliesController.target.Remove(other.gameObject);
                    _positionOnList--;
                    break;
            }
            
        }
    }
}
