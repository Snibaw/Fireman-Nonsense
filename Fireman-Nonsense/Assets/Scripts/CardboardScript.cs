using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardboardScript : MonoBehaviour
{
    private Animator animator;

    private void Start() 
    {
        animator = GetComponent<Animator>();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if (other.name == "Water Steam")
        {
            StartCoroutine(FadeAway());
        }
    }
    private IEnumerator FadeAway()
    {
        int rd = (int)Mathf.Round(Random.Range(0.15f,0.3f)*10);
        Destroy(gameObject,rd);
        yield return new WaitForSeconds(rd-1f);
        animator.SetTrigger("FadeAway");
    }
}
