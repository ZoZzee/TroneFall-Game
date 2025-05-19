using UnityEngine;

public class BildingPlan : MonoBehaviour
{
    public int goldAmount;

    [Header("References")]

    [SerializeField]private GameObject _referens;
    [SerializeField]private GameObject _flag;

    private bool _isBuilt = false;

    public bool _isPlayerNearby;

    private GoldManager goldManager;

    private void Awake()
    {
        goldManager = GoldManager.instance;
    }

    private void Update()
    {
       if(!_isBuilt &&
            _isPlayerNearby &&
            Input.GetKeyDown(KeyCode.E) &&
            goldManager.EnoughGold(goldAmount))
       {
            Build();
       }
    }

    private void Build()
    {
        _referens.SetActive(true);
        _isBuilt = true;
        _flag.SetActive(false);
        goldManager.MinusGold(goldAmount);
    }

}
