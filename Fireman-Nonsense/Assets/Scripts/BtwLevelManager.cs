using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BtwLevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text RangeUpgradeLevel;
    [SerializeField] private TMP_Text RangeUpgradeCost;
    [SerializeField] private TMP_Text DamageUpgradeLevel;
    [SerializeField] private TMP_Text DamageUpgradeCost;
    
    
    private int money;
    private int damageLevel;
    private int rangeLevel;
    private int damageCost;
    private int rangeCost;
    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString();
        
        damageLevel = PlayerPrefs.GetInt("DamageLevel",1);
        DamageUpgradeLevel.text = damageLevel.ToString();
        UpdateDamageCostText();

        rangeLevel = PlayerPrefs.GetInt("RangeLevel",1);
        RangeUpgradeLevel.text = rangeLevel.ToString();
        UpdateRangeCostText();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level"+PlayerPrefs.GetInt("LevelActuel",1).ToString());
    }
    public void BuyRangeUpgrade()
    {
        if(money >= rangeCost)
        {
            // Update Money
            money -= rangeCost;
            moneyText.text = money.ToString();

            // Update range text
            rangeLevel++;
            RangeUpgradeLevel.text = rangeLevel.ToString();
            UpdateRangeCostText();

            // Update PlayerPrefs
            PlayerPrefs.SetInt("RangeLevel",rangeLevel);
            PlayerPrefs.SetInt("Money",money);
        }
    }
    private void UpdateRangeCostText()
    {
        rangeCost = (int) Mathf.Round((rangeLevel*100*1.3f)/10)*10;
        RangeUpgradeCost.text = rangeCost.ToString();
    }
    public void BuyDamageUpgrade()
    {
        if(money >= damageCost)
        {
            // Update Money
            money -= damageCost;
            moneyText.text = money.ToString();

            // Update damage text
            damageLevel++;
            DamageUpgradeLevel.text = damageLevel.ToString();
            UpdateDamageCostText();

            // Update PlayerPrefs
            PlayerPrefs.SetInt("DamageLevel",damageLevel);
            PlayerPrefs.SetInt("Money",money);
        }
    }
    private void UpdateDamageCostText()
    {
        damageCost = (int) Mathf.Round((damageLevel*100*1.7f)/10)*10;
        DamageUpgradeCost.text = damageCost.ToString();
    }
}
