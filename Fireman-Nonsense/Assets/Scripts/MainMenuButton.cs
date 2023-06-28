using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void BtwLevelScene()
    {
        SceneManager.LoadScene("BtwLevelScene");
    }
}
