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
    private void Start()
    {
    #if UNITY_ANDROID
        PlayGamesPlatform.Instance.Authenticate(delegate(SignInStatus status) { });
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool succes) => { });
    #endif
    }
    public void ShowLeaderboardUI()
    {
        Social.ShowLeaderboardUI();
    }
    // [SerializeField] private TMP_Text statusTxt;

    // private void Awake() {
    //     PlayGamesPlatform.Activate();
    //     SignInOnStart();
    // }
    // // Start is called before the first frame update
    // internal void SignInOnStart()
    // {
    //     PlayGamesPlatform.Instance.Authenticate(authResult => 
    //     {
    //         if(authResult == SignInStatus.Success)
    //         {
    //             PlayGamesPlatform.Instance.RequestServerSideAccess(true, code => { SetAuthenticatingText(true); });
    //         }
    //         else
    //         {
    //             SetAuthenticatingText(false);
    //         }
    //     });
    // }

    // internal void SetAuthenticatingText(bool isAuthenticated)
    // {
    //     statusTxt.text = isAuthenticated ? "Authenticated" : "Authentication Failed";
    // }

    // internal void ConfigureGPGS()
    // {
    //     clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    // }

    // internal void SignIntoGPGPS(SignInInteractivity interactivity, PlayGamesClientConfiguration configuration )
    // {
    //     configuration = clientConfiguration;
    //     PlayGamesPlatform.InitializeInstance(configuration);
    //     PlayGamesPlatform.Activate();

    //     PlayGamesPlatform.Instance.Authenticate(interactivity, (code) => 
    //     {
    //         statusTxt.text = "Authenticating...";
    //         if(code == SignInStatus.Success)
    //         {
    //             statusTxt.text = "Authenticated";
    //             descriptionTxt.text = "Welcome " + Social.localUser.userName + "You have an id of " + Social.localUser.id;
    //         }
    //         else
    //         {
    //             statusTxt.text = "Authentication Failed";
    //             descriptionTxt.text = "Failed to authenticate for reason " + code.ToString();
    //         }
                
            
    //     });
    // }

    // public void BasicSignInBtn()
    // {
    //     SignIntoGPGPS(SignInInteractivity.CanPromptAlways, clientConfiguration);
    // }
    // public void BasicSignOutBtn()
    // {
    //     PlayGamesPlatform.Instance.SignOut();
    //     statusTxt.text = "Signed Out";
    // }
}
