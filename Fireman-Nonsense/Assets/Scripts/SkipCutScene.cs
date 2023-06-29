using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCutScene : MonoBehaviour
{
    [SerializeField] private GameObject EndClip;
    [SerializeField] private GameObject MoveCameraToFloor;
    [SerializeField] private GameObject SkipButton;
    [SerializeField] private CreateFloors Floors;
    private GameObject camHolder;
    // Start is called before the first frame update
    private void Start()
    {
        SkipButton.SetActive(true);
        camHolder = Camera.main.transform.parent.gameObject;
        camHolder.GetComponent<MainCamera>().enabled = false;
    }

    public void SkipScene()
    {
        Floors.instantSpawn = true;
        EndClip.SetActive(false);
        SkipButton.SetActive(false);
        StartCoroutine(WaitForFloorsToBeSpawn());
    }
    private IEnumerator WaitForFloorsToBeSpawn()
    {
        yield return new WaitForSeconds(0.3f);
        MoveCameraToFloor.SetActive(true);
    }

    
}
