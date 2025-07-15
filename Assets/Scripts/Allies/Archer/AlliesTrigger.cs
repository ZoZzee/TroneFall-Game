using UnityEngine;

public class AlliesTrigger : MonoBehaviour
{
    [SerializeField] private AlliesController _alliesController;

    public TriggerPriority currentTriggerPriority;
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
                    //_alliesController.attack = true;
                    break;
                case TriggerPriority.priority1:
                    if (_alliesController.target[0] == _alliesController._targetPoint)
                    {
                        _alliesController.target.Remove(_alliesController._targetPoint);
                    }
                    _alliesController.healthManagers.Add(other.GetComponent<HealthManager>());
                    _alliesController.target.Add(other.transform);
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
                    _alliesController.target.Remove(other.transform);
                    break;
            }
            
        }
    }
}
