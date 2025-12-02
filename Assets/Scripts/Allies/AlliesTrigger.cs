using UnityEngine;

public class AlliesTrigger : MonoBehaviour
{
    [SerializeField] private Bot _bot;

    public TriggerPriority currentTriggerPriority;

    public byte _positionOnList = 0;
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

                    _bot.buildsHealth.Add(other.GetComponent<HealthManager>());
                    _bot.builds.Add(other.gameObject);
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
                    _bot.buildsHealth.Remove(other.GetComponent<HealthManager>());
                    _bot.builds.Remove(other.gameObject);
                    break;
            }
            
        }
    }
}
