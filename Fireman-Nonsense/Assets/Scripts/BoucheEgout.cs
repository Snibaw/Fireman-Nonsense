using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucheEgout : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    private GameObject camHolder;
    private PauseMenuManager pauseMenuManager;
    private bool playerHit = false;

    private void Start() {
        animator = GetComponent<Animator>();
        camHolder = Camera.main.transform.parent.gameObject;
        player = GameObject.Find("Player");
        pauseMenuManager = GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>();
        
    }
    private void Update()
    {
        if(playerHit)
        {
            MoveThePlayerToTheGameObject();
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            animator.SetTrigger("Hit");
            StartCoroutine(HitManhole());
        }
    }
    private IEnumerator HitManhole()
    {
        playerHit = true;
        player.GetComponent<playerInput>().canMove = false;
        player.GetComponent<Rigidbody>().velocity /=8;
        player.GetComponent<CapsuleCollider>().enabled = false;
        camHolder.GetComponent<MainCamera>().enabled = false;
        Vibrator.Vibrate(Vibrator.vibrateTimeDamage);
        yield return new WaitForSeconds(1.5f);
        pauseMenuManager.OpenEndOfLevel(true, false);
    }
    private void MoveThePlayerToTheGameObject()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.transform.position = Vector3.MoveTowards(player.transform.position, targetPosition, 0.03f);
    }
}
