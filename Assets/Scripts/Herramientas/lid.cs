using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class lid : MonoBehaviour
{
    [Header("CONFIG")]
    public string etiquetaBote;

    private void Start()
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(etiquetaBote))
        {

            foreach (var hand in Player.instance.hands)
            {
                hand.DetachObject(other.gameObject);
            }

            this.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = other.transform.parent;

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;

        }
    }

    

}
