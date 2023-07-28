using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    public GameObject[] hats;
    // Start is called before the first frame update
    void Start()
    {
        ChooseHat(PlayerPrefs.GetInt("Hat",0));
    }

    public void ChooseHat(int i)
    {
        foreach(GameObject hat in hats)
        {
            hat.SetActive(false);
        }
        hats[i].SetActive(true);
    }
}
