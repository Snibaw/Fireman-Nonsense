using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCameraToFloor : MonoBehaviour
{
    private GameObject camHolder;
    [SerializeField] private int floor;
    private PauseMenuManager pauseMenuManager;
    private Transform target;
    private GameObject flame;
    private int floorNumber;
    private playerInput playerInput;
    private int maxFloor;
    private GameManager gameManager;
    private bool isEndOfLevelOpen = false;
    // Start is called before the first frame update

    void Start()
    {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
        maxFloor = PlayerPrefs.GetInt("MaxFloor",50);
        floor = (int) Mathf.Min(maxFloor-1, playerInput.currentMana/25);
        target= GameObject.Find("Floor ("+floor+")").transform;
        camHolder = GameObject.Find("Main Camera").transform.parent.gameObject;
        pauseMenuManager = GameObject.Find("LevelCanvas").transform.GetChild(2).GetComponent<PauseMenuManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isEndOfLevelOpen) return;
        //Smoothly move the camera towards that target position
        camHolder.transform.position = Vector3.Lerp(camHolder.transform.position, new Vector3(camHolder.transform.position.x, target.position.y, camHolder.transform.position.z), 0.02f);

        // Create a raycast from the cameraHolder to the z position
        RaycastHit hit;
        if (Physics.Raycast(camHolder.transform.position, camHolder.transform.forward, out hit))
        {
            floorNumber = int.Parse(hit.transform.parent.gameObject.name[7..^1].ToString());
            hit.transform.parent.gameObject.GetComponent<BuildingFloorBehaviour>().GetHitAndChangeColor();
            GameObject flame = GameObject.Find("Flame ("+floorNumber+")");
            if(flame != null) flame.SetActive(false);
            if(floorNumber == floor)
            {
                StartCoroutine(WaitAndShowMenu());
            }
        }

    }
    private IEnumerator WaitAndShowMenu()
    {
        isEndOfLevelOpen = true;
        yield return new WaitForSeconds(2f);
        pauseMenuManager.OpenEndOfLevel(false);
        gameManager.EarnMoney(100*PlayerPrefs.GetInt("Level", 1)*(1+floor/10));
        gameManager.UpdateTextTopLeftCorner();

    }
}