using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Plug : MonoBehaviour
{
    [Header("CONFIG")]
    public float Something;

    [Header("REFERENCE")]
    public Transform trans_plugHose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HoseEdge")){

            foreach (var hand in Player.instance.hands){       
                    hand.DetachObject(other.gameObject);
            }

            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = trans_plugHose.position;
            other.transform.rotation = trans_plugHose.rotation;
        } 
    }

}