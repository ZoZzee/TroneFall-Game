
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{

    public List<GameObject> buildings;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Building")||
            other.CompareTag("Construction"))
        {
            if (other.GetComponent<BuildingPlan>()._nextLevel != null)
            {
                buildings.Add(other.gameObject);
            }

            
        }
        if (other.CompareTag("Level"))
        {
            buildings.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Building")||
            other.CompareTag("Construction")
            ||other.CompareTag("Level"))
        {
            buildings.Remove(other.gameObject);
        }
    }
}
