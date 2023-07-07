using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuestManager: MonoBehaviour
{
    public Quest[] questPrefab;
    private int money;

    [SerializeField] private QuestDisplay[] questDisplay;
    [SerializeField] private TMP_Text moneyText;
    private void Start() {
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
                    money += questDispayElt.quest.rewardAmount;
                    PlayerPrefs.SetInt("Money", money);
                    UpdateMoneyText();
                }
                // else if(questDisplay.quest.rewardType == "Item")
                // {
                //     PlayerPrefs.SetInt("Item" + questDisplay.quest.rewardAmount, PlayerPrefs.GetInt("Item" + questDisplay.quest.rewardAmount, 0) + 1);
                // }
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
        money = PlayerPrefs.GetInt("Money", 0);
        moneyText.text = money.ToString();
    }
    private void ResetQuest(Quest quest)
    {
        quest.progress = 0;
        quest.completed = false;
    }
}
