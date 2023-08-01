using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    public GameObject[] backpacks;
    // Start is called before the first frame update
    void Start()
    {
        ChooseBackpack(PlayerPrefs.GetInt("Backpack",0));
    }

    public void ChooseBackpack(int i)
    {
        foreach(GameObject backpack in backpacks)
        {
            backpack.SetActive(false);
        }
        backpacks[i].SetActive(true);
    }
}
