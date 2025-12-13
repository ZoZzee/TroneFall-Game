using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public GameObject mainBuilding;

    public List<Building> finishedBuildings;
    public List<Building> destroyedBuildings;
    public List<GameObject> wallDestroyed;


    private DayNightManager _dayNightManager;

    public static BuildingsManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _dayNightManager = DayNightManager.instance;
        _dayNightManager.onDayStart.AddListener(OnDayStart);
    }

    public void OnDayStart()
    {
        BuildDestroyedBuildings();
    }

    private void BuildDestroyedBuildings()
    {
        for (int i = 0; i < destroyedBuildings.Count; i++)
        {
            destroyedBuildings[i].buildingPlan.Build(false);
        }
        for(int i = 0; i < wallDestroyed.Count; i++)
        {
            wallDestroyed[i].SetActive(true);
        }
        wallDestroyed.Clear();
        destroyedBuildings.Clear();
    }
}
