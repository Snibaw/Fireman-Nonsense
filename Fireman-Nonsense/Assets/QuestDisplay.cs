using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    public Quest quest;
    public int missionNumber;
    public int lastQuestNumber;
    public int attributedQuestNumber;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private TMP_Text rewardText;
    [SerializeField] private GameObject rewardIcon;
    [SerializeField] private GameObject completedIcon;
    [SerializeField] private Slider progressBar;
    private int[] usedQuestNumber;
    private int[] possibleQuestNumber;

    private QuestManager questManager;

    // Start is called before the first frame update
    void Start()
    {
        missionNumber = int.Parse(gameObject.name[9..^1].ToString());
        questManager = transform.parent.GetComponent<QuestManager>();
        
        if(quest == null)
            StartCoroutine(WaitTimeBeforeChoosing());
        else
            InitialiseQuest();
       
    }
    public IEnumerator WaitTimeBeforeChoosing()
    {
        attributedQuestNumber = PlayerPrefs.GetInt("Quest" + missionNumber, -1);
        if(attributedQuestNumber == -1) // Choose a random quest
        {
            yield return new WaitForSeconds(0.01f+missionNumber/100f);
            ChooseAvailableQuest();
        }
        else // Choose the attributed quest (priority)
        {
            quest = questManager.questPrefab[attributedQuestNumber];
        }
        InitialiseQuest();
    }
    private void InitialiseQuest()
    {
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        progressText.text = quest.progress + "/" + quest.goal;
        rewardText.text = "x"+quest.rewardAmount;
        rewardIcon.GetComponent<Image>().sprite = quest.rewardIcon;
        completedIcon.SetActive(quest.completed);

        progressBar.maxValue = quest.goal;
        progressBar.value = quest.progress;
    }
    private void ChooseAvailableQuest()
    {
        int numberOfDeclaredQuest = 0;
        usedQuestNumber = new int[4];
        for (int i = 0; i < 4; i++)
        {
            usedQuestNumber[i] = PlayerPrefs.GetInt("Quest" + i, -1);
            if (usedQuestNumber[i] != -1)
                numberOfDeclaredQuest++;
        }

        possibleQuestNumber = new int[questManager.questPrefab.Length-numberOfDeclaredQuest];
        int j = 0;
        for (int i = 0; i < questManager.questPrefab.Length; i++)
        {
            if (!IsInArray(i, usedQuestNumber) && i != lastQuestNumber)
            {
                possibleQuestNumber[j] = i;
                j++;
            }
        }

        int rdNumber = Random.Range(0, possibleQuestNumber.Length);
        quest = questManager.questPrefab[possibleQuestNumber[rdNumber]];
        PlayerPrefs.SetInt("Quest" + missionNumber, possibleQuestNumber[rdNumber]);
    }

    private bool IsInArray(int number, int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (number == array[i])
                return true;
        }
        return false;
    }
}

