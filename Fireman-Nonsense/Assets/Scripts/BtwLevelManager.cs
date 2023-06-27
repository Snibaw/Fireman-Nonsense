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
}
