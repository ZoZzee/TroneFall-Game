using TMPro;
using Unity.AI.Navigation.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("HZ")]
    private bool _startBuildsng;

    [Header("Referenses")]
    private DayNightManager _dNManager;
    private GameObject _closestBuild;
    private BuildingPlan _buildingPlan;
    [SerializeField] private InteractionTrigger _interactionTriger;

    [Header("Roll_UI")]
    [SerializeField] private GameObject _obgectUI;
    [SerializeField] private Image _roll;


    [Header("Timer")]
    private float spaceHoldTime;
    [SerializeField] private float _timeToWaitForTheNight = 250;
    [SerializeField] private float _constructionWaitingTime = 150;

    private void Start()
    {
        _dNManager = DayNightManager.instance;

        _obgectUI.SetActive(false);

    }
    private void Update()
    {
        CheckSpaseButton();
    }
    private void CheckSpaseButton()
    {
        if (_dNManager.dayStart == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CheckDistanseToBilding();
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if(_closestBuild != null)
                {
                    _buildingPlan.buildRol.fillAmount = 0.001f;
                }
                _obgectUI.SetActive(false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (_startBuildsng)
                {
                    _buildingPlan.buildRol.fillAmount = (spaceHoldTime / (_constructionWaitingTime / 100)) / 100;
                    if (spaceHoldTime >= _constructionWaitingTime)
                    {
                        Build();
                        spaceHoldTime = 0;
                        return;
                    }
                }
                else
                {
                    _roll.fillAmount = (spaceHoldTime / (_timeToWaitForTheNight / 100)) / 100;
                    if (!_startBuildsng && spaceHoldTime >= _timeToWaitForTheNight)
                    {
                        _dNManager.StartNight();
                        spaceHoldTime = 0;
                        _obgectUI.SetActive(false);
                    }
                }
                spaceHoldTime += Time.deltaTime * 60;
            }
            else
            {
                if (spaceHoldTime > 0)
                {
                    spaceHoldTime = 0;
                }
            }

        }
    }
    private void CheckDistanseToBilding()
    {
        if (_interactionTriger.buildings.Count < 1)
        {
            _startBuildsng = false;
            _obgectUI.SetActive(true);
        }
        else
        {
            _closestBuild = null;

            for (int i = 0; i < _interactionTriger.buildings.Count; i++)
            {
                GameObject build = _interactionTriger.buildings[i];
                _buildingPlan = build.GetComponent<BuildingPlan>();
                if (_closestBuild == null ||
                    Vector3.Distance(build.transform.position, transform.position) < Vector3.Distance(_closestBuild.transform.position, transform.position)
                     && _buildingPlan.goldManager.EnoughGold(_buildingPlan.goldManager.gold - _buildingPlan.costBuildings))
                {
                    _closestBuild = build;
                }
            }
            _buildingPlan = _closestBuild.GetComponent<BuildingPlan>();
            _startBuildsng = true;
            _buildingPlan.rollFilling.SetActive(true);
        }
    }
    private void Build()
    {
        if (_interactionTriger.buildings.Count <= 0)
        {
            return;
        }
        if (_buildingPlan.goldManager.EnoughGold(_buildingPlan.goldManager.gold - _buildingPlan.costBuildings))
        {
            _buildingPlan.buildRol.fillAmount = 0.001f;
            _buildingPlan.rollFilling.SetActive(false);
            _buildingPlan.Build(true);   ////////

            _interactionTriger.buildings.Remove(_closestBuild);

            _closestBuild = null;
        }

    }
}
