using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayerAnimatorCinematic : MonoBehaviour
{
    private Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        playerAnimator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
