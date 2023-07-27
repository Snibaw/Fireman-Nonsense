using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_Text logText;
    public TMP_InputField scoreInput;

    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    public void DoLeaderboardPost(int _score)
    {
        Social.ReportScore(_score, GPGSIds.leaderboard_test_leaderboard,
            (bool success) => 
            {
                if(success)
                    logText.text = "Score Posted of"+_score;
                else
                    logText.text = "Score Failed";
            }
        );
    }
    public void PostLeaderboardBTN()
    {
        DoLeaderboardPost(int.Parse(scoreInput.text));
    }
}
