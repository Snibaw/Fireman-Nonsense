using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private float manaGain = 10;
    private Animator animator;
    private bool playerHit= false;
    private GameObject player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(playerHit)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0,1f,0), 0.05f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHit = true;
            player = other.gameObject;
            player.GetComponent<playerInput>().ChangeCurrentMana(manaGain);
            animator.SetTrigger("Hit");
            // animator.SetTrigger("Hit");
            Destroy(gameObject,0.3f);
        }
    }
}
