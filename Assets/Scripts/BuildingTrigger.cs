using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    public BildingPlan bildingPlan;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bildingPlan.enabled = true;
            bildingPlan._isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bildingPlan.enabled = false;
            bildingPlan._isPlayerNearby = false;
        }
    }

}
