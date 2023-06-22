using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBuildingManager : MonoBehaviour
{
    [SerializeField] private GameObject EndClip;
    [SerializeField] private CreateFloors Floors;
    private bool hasTrigger= false;
    // Start is called before the first frame update
    void Start()
    {
        EndClip.SetActive(false);
    }
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "Player")
        {
            if(hasTrigger) return;
            hasTrigger = true;
            other.gameObject.GetComponent<playerInput>().canMove = false;
            Floors.StartCoroutine("SpawnFloorWithDelay");
            EndClip.SetActive(true);
        }
    }
}
