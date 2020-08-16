using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrainDetector : MonoBehaviour
{
    [Header("CONFIG")]
    public string GrainType = "Granos";

    [Header("EVENTS")]
    public UnityEvent grainDetection;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Granos")){
            grainDetection.Invoke();
        }
    }

}