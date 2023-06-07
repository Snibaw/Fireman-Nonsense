using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateWallBehaviour : MonoBehaviour
{
    [SerializeField] private GateBehaviour Gate;
    private playerInput playerInput;

    private void Start() {
        playerInput = GameObject.Find("Player").GetComponent<playerInput>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            if(Gate.GetgateName() == "Fire Rate")
            {
                playerInput.ChangeParticleRateOverTimeValues(Gate.Getvalue(),Gate.GetisValueMultiplier());
                playerInput.UpdateParticleRateOverTime();
            }
            if(Gate.GetgateName() == "Damage")
            {
                playerInput.ChangeDamageValues(Gate.Getvalue(),Gate.GetisValueMultiplier());
            }
            Destroy(Gate.gameObject);
        }

    }
}
