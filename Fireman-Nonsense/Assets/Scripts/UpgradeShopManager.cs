using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UpgradeShopManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text[] priceText;
    [SerializeField] private TMP_Text[] levelText;
    [SerializeField] private float[] priceMultiplier;
    [SerializeField] private float[] priceAddition;
    [SerializeField] private float[] upgradeMultiplier;
    [SerializeField] private float[] upgradeAddition;
    private AudioSource audioSource;

    private int[] levelList;
    private int[] priceList;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        UpdateMoney();

        priceList = new int[6];
        levelList = new int[6];

        UpdateLevelList();
        UpdateText();
    }

    private void UpdateLevelList()
    {
        for( int i = 0; i < levelList.Length; i++)
        {
            levelList[i] = PlayerPrefs.GetInt("UpgradeLevel" + i.ToString(),0);
        }
    }
    private void UpdateText()
    {
        for (int i = 0; i < levelText.Length; i++)
        {
            levelText[i].text = levelList[i].ToString();
            priceList[i] = (int)Mathf.Round((priceAddition[i] + 100 * levelList[i] * priceMultiplier[i])/10)*10;
            priceText[i].text = priceList[i].ToString();
        }
    }
    public void BuyUpgrade(int index)
    {
        if (money >= priceList[index])
        {
            audioSource.Play();

            money -= priceList[index];

            PlayerPrefs.SetInt("Money", money);

            levelList[index]++;

            PlayerPrefs.SetInt("UpgradeLevel" + index.ToString(), levelList[index]);
            
            PlayerPrefs.SetFloat("UpgradeValue" + index.ToString(), (upgradeAddition[index] + upgradeMultiplier[index] * levelList[index]));

            UpdateMoney();

            UpdateText();

        }
    }
    private void UpdateMoney()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText.text = money.ToString();
    }
    public void BtwLevelScene()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
}
