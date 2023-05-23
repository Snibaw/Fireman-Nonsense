using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float trapActivationTime;
    Animator animator;
    private float timeRemaining;
    bool playerOnTrap = false;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = trapActivationTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;
        if(timeRemaining <= 0)
        {
            //trigger animation
            animator.SetTrigger("Activate");
            timeRemaining = trapActivationTime;
            if(GameObject.Find("Player") != null && playerOnTrap)
            {
                GameObject.Find("Player").GetComponent<PlayerHealth>().TakeDamage(10);
            }
        }
    }
   void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Player touched the trap");
        if(collision.gameObject.tag == "Player")
        {
            playerOnTrap = true;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerOnTrap = false;
        }
    }
}
