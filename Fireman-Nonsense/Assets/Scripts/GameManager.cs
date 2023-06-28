using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isBossScene = false;
    private int money;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text moneyTextTopLeftCorner;

    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = "0";
        if(!isBossScene)
        {
            UpdateTextTopLeftCorner();
        }
    }
    public void EarnMoney(int moneyInput)
    {
        money += moneyInput;
        moneyText.text = moneyInput.ToString();
        PlayerPrefs.SetInt("Money", money);
        moneyText.GetComponent<Animator>().SetTrigger("EarnMoney");
    }
    public void UpdateTextTopLeftCorner()
    {
        moneyTextTopLeftCorner.text = money.ToString();
    }
}
