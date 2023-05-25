using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    private int levelNumber;
    public void SetLevel(int i)
    {
        levelNumber = i;
        gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text = i.ToString();

    }
    public void LoadLevel()
    {
        SceneManager.LoadScene("Level" + levelNumber);
    }
}
