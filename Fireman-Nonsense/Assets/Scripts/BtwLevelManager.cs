using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BtwLevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        money = PlayerPrefs.GetInt("Money",0);
        moneyText.text = money.ToString();
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Level"+PlayerPrefs.GetInt("LevelActuel",1).ToString());
    }
}
