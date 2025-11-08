
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{

    public List<GameObject> buildings;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            if (other.GetComponent<BuildingPlan>()._nextLevel != null)
            {
                buildings.Add(other.gameObject);
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building"))
        {
            buildings.Remove(other.gameObject);
        }
    }
}
