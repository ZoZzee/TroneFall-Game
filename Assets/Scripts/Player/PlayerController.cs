
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("HZ")]
    private bool _startBuildsng = false;

    [Header("Referenses")]

    private bool _isBuilding = false;
    private GameObject _closestBuild;
    private ChooseLevel _chooseLevel;
    private BuildingPlan _buildingPlan;
    [SerializeField] private InteractionTrigger _interactionTriger;
    private DayNightManager _dNManager;

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
                if (_closestBuild != null)
                {
                    _buildingPlan.buildRol.fillAmount = 0.001f;
                }
                else
                {
                    if(_chooseLevel)
                    {
                        _chooseLevel._obgectUI.SetActive(false);
                    }
                }
                _startBuildsng = false;
                _isBuilding = false;
                _obgectUI.SetActive(false);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                if (_chooseLevel)
                {
                    _chooseLevel._roll.fillAmount = (spaceHoldTime / (_constructionWaitingTime / 100)) / 100;
                    if (spaceHoldTime >= _constructionWaitingTime)
                    {
                        loadNewLevel();
                        spaceHoldTime = 0;
                        return;
                    }
                }
                else
                {
                    if (_startBuildsng && !_isBuilding)
                    {
                        _buildingPlan.buildRol.fillAmount = (spaceHoldTime / (_constructionWaitingTime / 100)) / 100;
                        if (spaceHoldTime >= _constructionWaitingTime)
                        {
                            _isBuilding = true;
                            Build();
                            spaceHoldTime = 0;
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
        _closestBuild = null;

        if (_interactionTriger.levels.Count > 0)
        {
            _chooseLevel = _interactionTriger.levels[0].GetComponent<ChooseLevel>();
            _chooseLevel._obgectUI.SetActive(true);
        }
        else if (_interactionTriger.buildings.Count < 1)
        {
            _startBuildsng = false;
            _obgectUI.SetActive(true);
            return;
        }
        else
        {
            _closestBuild = null;


            GameObject build = null;
            BuildingPlan buildingPlanB;
            for (int i = 0; i < _interactionTriger.buildings.Count; i++)
            {
                build = _interactionTriger.buildings[i];
                buildingPlanB = build.GetComponent<BuildingPlan>();
                if (_closestBuild == null ||
                Vector3.Distance(build.transform.position, transform.position) < Vector3.Distance(_closestBuild.transform.position, transform.position)
                && buildingPlanB.goldManager.EnoughGold(buildingPlanB.costBuildings))
                {
                    _closestBuild = build;
                }
            }

            _buildingPlan = _closestBuild.GetComponent<BuildingPlan>();

            if (_buildingPlan.goldManager.EnoughGold(_buildingPlan.costBuildings))
            {
                _startBuildsng = true;
                _buildingPlan.rollFilling.SetActive(true);
            }
            else
            {
                _closestBuild = null;
                _startBuildsng = false;
            }
        }
    }
    
    private void Build()
    {
        if (_interactionTriger.buildings.Count <= 0)
        {
            return;
        }
        
        if (_buildingPlan.goldManager.EnoughGold(_buildingPlan.costBuildings))
        {

            _interactionTriger.buildings.Remove(_closestBuild);
            _buildingPlan.buildRol.fillAmount = 0.001f;
            _buildingPlan.rollFilling.SetActive(false);
            _buildingPlan.Build(true);   ////////

            _closestBuild = null;
        }

    }
    private void loadNewLevel()
    {
        if (_chooseLevel != null)
        {
            Debug.Log("Vse dobre");
            _chooseLevel.LoadScene(_chooseLevel.indexLevel);
            _chooseLevel = null;

        }
    }
}
