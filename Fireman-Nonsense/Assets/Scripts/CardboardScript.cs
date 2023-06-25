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

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {
            HitByRay();
        }
    }
    //Whenever it hits by a raycast
    void HitByRay () {
        GetKnockedBack();
        StartCoroutine(FadeAway());
    }


    private IEnumerator FadeAway()
    {
        int rd = (int)Mathf.Round(Random.Range(0.15f,0.3f)*10);
        Destroy(gameObject,rd);
        yield return new WaitForSeconds(rd-1f);
        animator.SetTrigger("FadeAway");
    }
    private void GetKnockedBack()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*0.0003f,ForceMode.Impulse);
    }
}
