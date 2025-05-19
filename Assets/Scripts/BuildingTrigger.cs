using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    public BildingPlan bildingPlan;

    [HideInInspector] public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bildingPlan.enabled = true;
            bildingPlan._isPlayerNearby = true;

            playerTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bildingPlan.enabled = false;
            bildingPlan._isPlayerNearby = false;

            playerTransform = null;
        }
    }

}
