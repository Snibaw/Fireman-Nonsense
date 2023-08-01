using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class QuestManager: MonoBehaviour
{
    public Quest[] questPrefab;
    private long money;
    private int crystal;

    [SerializeField] private QuestDisplay[] questDisplay;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text crystalText;
    private void Start() {
        crystal = PlayerPrefs.GetInt("Crystal", 0);
        crystalText.text = crystal.ToString();
        UpdateMoneyText();

    }

    public void GoToBtwLevelScene()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
    public void Claim()
    {
        foreach(QuestDisplay questDispayElt in questDisplay)
        {
            if(questDispayElt.quest.completed)
            {
                if(questDispayElt.quest.rewardType == "Money")
                {
                    money += (int) (questDispayElt.quest.rewardAmount*(1+PlayerPrefs.GetFloat("UpgradeValue5", 0)));
                    PlayerPrefs.SetString("Money", money.ToString());
                    UpdateMoneyText();
                }
                else if(questDispayElt.quest.rewardType == "Crystal")
                {
                    crystal += questDispayElt.quest.rewardAmount;
                    PlayerPrefs.SetInt("Crystal", crystal);
                    crystalText.text = crystal.ToString();
                }
                ResetQuest(questDispayElt.quest);
                questDispayElt.lastQuestNumber = questDispayElt.attributedQuestNumber;
                PlayerPrefs.SetInt("Quest" + questDispayElt.missionNumber, -1);
                PlayerPrefs.SetInt("QuestInitialValue" + questDispayElt.missionNumber, -1);
                StartCoroutine(questDispayElt.WaitTimeBeforeChoosing());

            }
        }
    }

    private void UpdateMoneyText()
    {
        money = long.Parse(PlayerPrefs.GetString("Money","0"));
        moneyText.text = money.ToString();
        
    }
    private void ResetQuest(Quest quest)
    {
        quest.progress = 0;
        quest.completed = false;
    }
}
