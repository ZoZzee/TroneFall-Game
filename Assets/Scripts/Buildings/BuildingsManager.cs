using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public Transform mainBuilding;

    public List<Building> finishedBuildings;
    public List<Building> destroyedBuildings;


    private DayNightManager _dayNightManager;
    private GoldManager _goldManager;

    public static BuildingsManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _goldManager = GoldManager.instance;
        _dayNightManager = DayNightManager.instance;
        _dayNightManager.onDayStart.AddListener(OnDayStart);
    }

    public void OnDayStart()
    {
        AddGold();
        BuildDestroyedBuildings();
    }

    private void BuildDestroyedBuildings()
    {
        for (int i = 0; i < destroyedBuildings.Count; i++)
        {
            destroyedBuildings[i].buildingPlan.Build(false);
        }

        destroyedBuildings.Clear();
    }

    private void AddGold()
    {
        int goldAmount = 0;

        for (int i = 0; i < finishedBuildings.Count; i++)
        {
            goldAmount += finishedBuildings[i].goldAtDayStart;
        }

        _goldManager.PlusGold(goldAmount);
    }
}
