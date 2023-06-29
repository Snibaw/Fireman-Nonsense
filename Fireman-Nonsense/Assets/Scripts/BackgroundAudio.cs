using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        // If there is more than one background audio, destroy the new one
        if (FindObjectsOfType<BackgroundAudio>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
}
