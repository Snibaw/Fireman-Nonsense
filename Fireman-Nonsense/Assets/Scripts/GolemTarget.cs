using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemTarget : MonoBehaviour
{
    //Change the alpha of the mesh renderer to make it transparent
    private MeshRenderer meshRenderer;
    private Color color;
    private float alpha = 0.5f;
    private bool isAttacking = false;
    private PauseMenuManager pauseMenuManager;

    private void Start()
    {
        pauseMenuManager = GameObject.Find("PauseMenu").GetComponent<PauseMenuManager>();
        meshRenderer = GetComponent<MeshRenderer>();
        color = meshRenderer.material.color;
        color.a = alpha;
        meshRenderer.material.color = color;
        StartCoroutine(WaitingBeforeAttack());

    }
    private IEnumerator WaitingBeforeAttack()
    {
        for(int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.25f);
            color.a = 0f;
            meshRenderer.material.color = color;
            yield return new WaitForSeconds(0.25f);
            color.a = alpha;
            meshRenderer.material.color = color;
        }
        yield return new WaitForSeconds(0.5f);
        isAttacking = true;
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other) {
        if(isAttacking)
        {
            if(other.gameObject.tag == "Player" && pauseMenuManager.gameEnded == false)
            {
                other.gameObject.GetComponent<PlayerInputBossLevel>().canMove = false;
                pauseMenuManager.OpenEndOfLevel(true,false);
            }
        }
    }
}
