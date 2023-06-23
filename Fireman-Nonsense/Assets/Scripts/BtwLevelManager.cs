using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BtwLevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text CapacityUpgradeLevel;
    [SerializeField] private TMP_Text CapacityUpgradeCost;
    [SerializeField] private TMP_Text FloorsUpgradeLevel;
    [SerializeField] private TMP_Text FloorsUpgradeCost;
    [SerializeField] private GameObject BtwLevelCinematic;
    [SerializeField] private GameObject[] DesactivateOnStart;
    
    
    private int money;
    private int FloorsLevel;
    private int CapacityLevel;
    private int FloorsCost;
    private int CapacityCost;
    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString();
        
        FloorsLevel = (PlayerPrefs.GetInt("MaxFloor",50)-50)/10;
        FloorsUpgradeLevel.text = FloorsLevel.ToString();
        UpdateFloorCostText();

        CapacityLevel = (PlayerPrefs.GetInt("MaxMana",1000)-1000)/120;
        CapacityUpgradeLevel.text = CapacityLevel.ToString();
        UpdateCapacityCostText();

        Debug.Log("MaxMana"+ PlayerPrefs.GetInt("MaxMana",1000));
        Debug.Log("MaxFloor" + PlayerPrefs.GetInt("MaxFloor",50));
    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelCoroutine());

    }
    private IEnumerator StartLevelCoroutine()
    {
        for(int i = 0; i < DesactivateOnStart.Length; i++)
        {
            DesactivateOnStart[i].SetActive(false);
        }
        BtwLevelCinematic.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("LevelRandom");
    }
    public void BuyCapacityUpgrade()
    {
        if(money >= CapacityCost)
        {
            // Update Money
            money -= CapacityCost;
            moneyText.text = money.ToString();

            // Update capacity text
            CapacityLevel++;
            CapacityUpgradeLevel.text = CapacityLevel.ToString();
            UpdateCapacityCostText();

            // Update PlayerPrefs
            PlayerPrefs.SetInt("MaxMana",1000 + CapacityLevel*120);
            PlayerPrefs.SetInt("Money",money);
        }
    }
    private void UpdateCapacityCostText()
    {
        CapacityCost = (int) Mathf.Round((CapacityLevel*100*1.3f)/10)*10;
        CapacityUpgradeCost.text = CapacityCost.ToString();
    }
    public void BuyFloorUpgrade()
    {
        if(money >= FloorsCost)
        {
            // Update Money
            money -= FloorsCost;
            moneyText.text = money.ToString();

            // Update damage text
            FloorsLevel++;
            FloorsUpgradeLevel.text = FloorsLevel.ToString();
            UpdateFloorCostText();

            // Update PlayerPrefs
            PlayerPrefs.SetInt("MaxFloor",50+FloorsLevel*10);
            PlayerPrefs.SetInt("Money",money);
        }
    }
    private void UpdateFloorCostText()
    {
        FloorsCost = (int) Mathf.Round((FloorsLevel*100*1.7f)/10)*10;
        FloorsUpgradeCost.text = FloorsCost.ToString();
    }
}
