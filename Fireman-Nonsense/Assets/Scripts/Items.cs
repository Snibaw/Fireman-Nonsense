using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private float manaGain = 10;
    private Animator animator;
    private bool playerHit= false;
    private GameObject player;
    [SerializeField] private bool isCrystal = false;

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
            if(manaGain > 0) PlayerPrefs.SetInt("PickUpWater",PlayerPrefs.GetInt("PickUpWater",0)+1);
            playerHit = true;
            player = other.gameObject;
            if(isCrystal) player.GetComponent<playerInput>().ChangeCrystal();
            else player.GetComponent<playerInput>().ChangeCurrentMana(manaGain, Vibrator.vibrateTimeItem);
            animator.SetTrigger("Hit");
            Destroy(gameObject,0.3f);
        }
    }
}
