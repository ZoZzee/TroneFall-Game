using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public int gold;

    public static GoldManager instance;

    [SerializeField]private TMP_Text _goldText;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        RefreshUI();
    }

    public void PlusGold(int count)
    {
        gold += count;
        RefreshUI();
    }

    public void MinusGold(int count)
    {

        gold -= count;
        RefreshUI();
    }

    public bool EnoughGold(int count)
    {
        if(gold >= count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RefreshUI()
    {
        _goldText.text = gold.ToString();
    }
}
