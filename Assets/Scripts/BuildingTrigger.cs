using UnityEngine;

public class BuildingTrigger : MonoBehaviour
{
    public BuildingPlan bildingPlan;

    [HideInInspector] public Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bildingPlan.enabled = true;
            bildingPlan._isPlayerNearby = true;

            playerTransform = other.transform;
        }

        if (other.CompareTag("Enemy"))
        {
            if(bildingPlan.isBuilt == true)
                bildingPlan.building.GetComponent<Building>().DestroyBuilding();
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
