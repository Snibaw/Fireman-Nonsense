using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class BtwLevelManager : MonoBehaviour
{
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private GameObject BtwLevelCinematic;
    [SerializeField] private GameObject[] DesactivateOnStart;
    [SerializeField] private GameObject[] ChangeModeButton;
    [SerializeField] private TMP_Text InfoText;
    
    private int crystal;
    private string money;
    private int mode;
    // Start is called before the first frame update
    void Start()
    {
        // If you reach max int
        if(PlayerPrefs.GetInt("Money",0) < 0) PlayerPrefs.SetInt("Money",0);

        // From a int to string in next update
        if(long.Parse(PlayerPrefs.GetString("Money","0")) == 0) PlayerPrefs.SetString("Money",PlayerPrefs.GetInt("Money").ToString());

        money = PlayerPrefs.GetString("Money","0");
        moneyText.text = money;

        crystal = PlayerPrefs.GetInt("Crystal",0);
        crystalText.text = crystal.ToString();

        mode = PlayerPrefs.GetInt("Mode",0);
        UpdateModeButton();
    }

    public void StartLevel()
    {
        PlayerPrefs.SetInt("Play",PlayerPrefs.GetInt("Play",0)+1);
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
        if(mode == 0) SceneManager.LoadScene("LevelRandom");
        else SceneManager.LoadScene("LevelInfini");
    }
    private void UpdateModeButton()
    {
        ChangeModeButton[mode].SetActive(false);
        ChangeModeButton[1-mode].SetActive(true);
        UpdateText();
    }
    public void ChangeMode(int mod)
    {
        mode = mod;
        PlayerPrefs.SetInt("Mode",mode);
        UpdateModeButton();
    }
    private void UpdateText()
    {
        string modeString = mode == 0 ? "Campagne" : "Infini";
        string levelString = mode == 0 ? "Niveau : "+ PlayerPrefs.GetInt("Level",1).ToString() : "High Score : "+ PlayerPrefs.GetInt("HighScore",0).ToString();

        InfoText.text = "Mode : " + modeString + "\n" + levelString;
    }
}
