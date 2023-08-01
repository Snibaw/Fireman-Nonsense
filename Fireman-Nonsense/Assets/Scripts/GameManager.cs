using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isBossScene = false;
    private long money;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text moneyTextTopLeftCorner;
    private int totalMoneyEarned = 0;
    [SerializeField] private TMP_Text crystalText;

    // Start is called before the first frame update
    void Start()
    {
        money = long.Parse(PlayerPrefs.GetString("Money","0"));
        moneyText.text = "0";
        if(!isBossScene)
        {
            UpdateTextTopLeftCorner();
            crystalText.text = PlayerPrefs.GetInt("Crystal",0).ToString();
        }
    }
    public void EarnMoney(int moneyInput)
    {
        PlayerPrefs.SetInt("MoneyEarned",PlayerPrefs.GetInt("MoneyEarned",0)+moneyInput);
        money += moneyInput;
        totalMoneyEarned += moneyInput;
        moneyText.text = totalMoneyEarned.ToString();
        PlayerPrefs.SetString("Money", money.ToString());
        moneyText.GetComponent<Animator>().SetTrigger("EarnMoney");
    }
    public void UpdateTextTopLeftCorner()
    {
        moneyTextTopLeftCorner.text = money.ToString();
    }
    public void UpdateCrystalText()
    {
        crystalText.text = PlayerPrefs.GetInt("Crystal",0).ToString();
        crystalText.transform.parent.GetComponent<Animator>().SetTrigger("Trigger");
    }
}
