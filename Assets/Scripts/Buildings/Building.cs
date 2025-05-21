using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Values")]
    public int goldAtDayStart;

    [Header("Health")]
    public int health;
    public int healthMax;

    public BuildingPlan buildingPlan;

    private BuildingsManager _buildingsManager;

    private void Start()
    {
        _buildingsManager = BuildingsManager.instance;

        _buildingsManager.finishedBuildings.Add(this);
    }

    private void OnEnable()
    {
        if (_buildingsManager?.finishedBuildings.Contains(this) == false)
        {
            _buildingsManager.finishedBuildings.Add(this);
        }
    }

    public void DestroyBuilding()
    {
        _buildingsManager.finishedBuildings.Remove(this);
        _buildingsManager.destroyedBuildings.Add(this);

        buildingPlan.isBuilt = false;

        gameObject.SetActive(false);
    }
}
