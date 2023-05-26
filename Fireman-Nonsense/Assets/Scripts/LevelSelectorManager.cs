using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelSelectorGrid;
    [SerializeField] private int testValue = 0;
    [SerializeField] private GameObject[] LevelPrefabs;
    [SerializeField] private int maxPageNumber = 1;
    [SerializeField] private int currentPageNumber = 1;
    [SerializeField] private int numberOfLevelsPerPage = 9;
    [SerializeField] private GameObject[] cubeIndicatingPage;
    [SerializeField] private Sprite[] cubeSprites;
    private int playerLevel;
        // Start is called before the first frame update
    void Start()
    {
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", testValue);
        Debug.Log("Player Level: " + playerLevel);
        UpdateLevelPrefabs();
    }

    private void UpdateLevelPrefabs()
    {
        for(int i = 1 + (numberOfLevelsPerPage * (currentPageNumber - 1)); i <= numberOfLevelsPerPage * currentPageNumber; i++)
        {
            if(i < playerLevel)
            {
                GameObject levelPrefab = Instantiate(LevelPrefabs[0], LevelSelectorGrid.transform);
                levelPrefab.GetComponent<LevelButton>().SetLevel(i);
            }
            else if(i == playerLevel)
            {
                GameObject levelPrefab = Instantiate(LevelPrefabs[1], LevelSelectorGrid.transform);
                levelPrefab.GetComponent<LevelButton>().SetLevel(i);
            }
            else
            {
                GameObject levelPrefab = Instantiate(LevelPrefabs[2], LevelSelectorGrid.transform);
            }
        }
    }
    public void ChangePage(int pageNumber)
    {
        if(currentPageNumber + pageNumber <= maxPageNumber && currentPageNumber + pageNumber > 0)
        {
            currentPageNumber = currentPageNumber + pageNumber;
            foreach (Transform child in LevelSelectorGrid.transform)
            {
                Destroy(child.gameObject);
            }
            UpdateLevelPrefabs();

            for(int i=0; i < cubeIndicatingPage.Length; i++)
            {
                if(i == currentPageNumber - 1)
                {
                    cubeIndicatingPage[i].GetComponent<Image>().sprite = cubeSprites[1];
                }
                else
                {
                    cubeIndicatingPage[i].GetComponent<Image>().sprite = cubeSprites[0];
                }
            }
        }
    }
}
