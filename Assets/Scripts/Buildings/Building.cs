using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Values")]
    public List<byte> goldAtDayStart;

    [Header("Referense")]

    public BuildingPlan buildingPlan;

    private BuildingsManager _buildingsManager;
    private DayNightManager _dayNightManager;

    [SerializeField]private GameObject _coin;

    private void Start()
    {
        _buildingsManager = BuildingsManager.instance;

        _buildingsManager.finishedBuildings.Add(this);

        _dayNightManager = DayNightManager.instance;
        _dayNightManager.onDayStart.AddListener(OnDayStart);
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

    public void OnDayStart()
    {
        byte golds = goldAtDayStart[buildingPlan._levelEnhancement];
        for (int i = 0; i < golds; i++)
        {
            Vector3 position = transform.position + new Vector3(Random.Range(1f, 2f), 0, Random.Range(1f, 2f));
            Instantiate(_coin, position, _coin.transform.rotation);
        }
        
    }
}
