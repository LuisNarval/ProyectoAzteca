using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PedestalMovil : MonoBehaviour
{
    [Header("CONFIG")]
    public string BagTag;

    [Header("REFERENCE")]
    public Transform trans_WaterBag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(BagTag)){

            GameObject bolsaAgua = other.transform.parent.parent.gameObject;

            foreach (var hand in Player.instance.hands){
                hand.DetachObject(bolsaAgua);
            }

            bolsaAgua.GetComponent<Rigidbody>().isKinematic = true;

            bolsaAgua.transform.position = trans_WaterBag.position;
            bolsaAgua.transform.rotation = trans_WaterBag.rotation;
            bolsaAgua.transform.parent = trans_WaterBag;
        }
    }

}
