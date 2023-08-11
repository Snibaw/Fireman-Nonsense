using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using TMPro;

public class GPGSManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private void Start()
    {
    #if UNITY_ANDROID
        PlayGamesPlatform.Instance.Authenticate(delegate(SignInStatus status) { });
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        //Recup save Anthony
        if(PlayGamesPlatform.Instance.localUser.id == "a_8362613363900685299")
        {
            text.text = "Coucou Anthony";
            if(PlayerPrefs.GetInt("Level",1) == 1)
            {
                PlayerPrefs.SetInt("Level",51);
                PlayerPrefs.SetInt("Crystal", 150);
                PlayerPrefs.SetString("Money", "9000000");
                PlayerPrefs.SetInt("UpgradeLevel0", 300);
                PlayerPrefs.SetInt("UpgradeLevel1", 300);
                PlayerPrefs.SetInt("UpgradeLevel2", 200);
                PlayerPrefs.SetInt("UpgradeLevel5", 250);
            }
            
        }
        Social.localUser.Authenticate((bool succes) => { });

        // Update Leaderboard
        Social.ReportScore(PlayerPrefs.GetInt("Play",0), GPGSIds.leaderboard_nombre_de_parties_joues, success => {Debug.Log(success ? "Reported score successfully" : "Failed to report score");});
        Social.ReportScore(PlayerPrefs.GetInt("HighScore",0), GPGSIds.leaderboard_meilleur_score_mode_infini, success => {Debug.Log(success ? "Reported score successfully" : "Failed to report score");});
        Social.ReportScore(PlayerPrefs.GetInt("Level",1), GPGSIds.leaderboard_plus_haut_niveau_mode_campagne, success => {Debug.Log(success ? "Reported score successfully" : "Failed to report score");});
        Social.ReportScore(long.Parse(PlayerPrefs.GetString("Money","0")), GPGSIds.leaderboard_argent_maximum, success => {Debug.Log(success ? "Reported score successfully" : "Failed to report score");});
        Social.ReportScore(PlayerPrefs.GetInt("QuetesTerminees",0), GPGSIds.leaderboard_qutes_termines, success => {Debug.Log(success ? "Reported score successfully" : "Failed to report score");});
    #endif
    }
    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
}
