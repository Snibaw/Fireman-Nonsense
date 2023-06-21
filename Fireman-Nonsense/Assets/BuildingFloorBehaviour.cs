using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFloorBehaviour : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = materials[0];
        Debug.Log(gameObject.name[7].ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            meshRenderer.material = materials[int.Parse(gameObject.name[7].ToString())+1];
        }
    }
}
