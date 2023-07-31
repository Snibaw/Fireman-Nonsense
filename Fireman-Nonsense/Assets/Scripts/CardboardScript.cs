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
        FadeAway();
    }


    private void FadeAway()
    {
        Destroy(gameObject,1f);
        animator.SetTrigger("FadeAway");
    }
    private void GetKnockedBack()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward*0.0003f,ForceMode.Impulse);
    }
}
