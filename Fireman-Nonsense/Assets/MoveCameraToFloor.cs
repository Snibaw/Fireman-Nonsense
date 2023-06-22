using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraToFloor : MonoBehaviour
{
    private GameObject camHolder;
    [SerializeField] private int floor;
    private Transform target;
    private GameObject flame;
    private int floorNumber;
    // Start is called before the first frame update
    void Start()
    {
        target= GameObject.Find("Floor ("+floor+")").transform;
        camHolder = GameObject.Find("Main Camera").transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Smoothly move the camera towards that target position
        camHolder.transform.position = Vector3.Lerp(camHolder.transform.position, new Vector3(camHolder.transform.position.x, target.position.y, camHolder.transform.position.z), 0.005f);

        // Create a raycast from the cameraHolder to the z position
        RaycastHit hit;
        if (Physics.Raycast(camHolder.transform.position, camHolder.transform.forward, out hit))
        {
            floorNumber = int.Parse(hit.transform.parent.gameObject.name[7..^1].ToString());
            hit.transform.parent.gameObject.GetComponent<BuildingFloorBehaviour>().GetHitAndChangeColor();
            GameObject flame = GameObject.Find("Flame ("+floorNumber+")");
            if(flame != null) flame.SetActive(false);
        }

    }
}
