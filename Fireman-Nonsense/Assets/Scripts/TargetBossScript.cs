using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBossScript : MonoBehaviour
{
    [SerializeField] private GolemBossBehaviour golemBossBehaviour;
    private void OnParticleCollision(GameObject other) 
    {
        if (other.name == "Water Steam")
        {
            golemBossBehaviour.TakeDamage(3f);
        }
    }
}
