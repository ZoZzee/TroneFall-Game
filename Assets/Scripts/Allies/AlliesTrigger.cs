using UnityEngine;

public class AlliesTrigger : MonoBehaviour
{
    [SerializeField] private Bot _bot;

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
                    _bot.canAttack = true;
                    break;
                case TriggerPriority.priority1:

                    _bot.targetHealth.Add(other.GetComponent<HealthManager>());
                    _bot.target.Insert(_positionOnList,other.gameObject);
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
                   _bot.canAttack = false;
                    break;
                case TriggerPriority.priority1:

                    _bot.targetHealth.Remove(other.GetComponent<HealthManager>());
                    _bot.target.Remove(other.gameObject);
                    _positionOnList--;
                    break;
            }
            
        }
    }
}
