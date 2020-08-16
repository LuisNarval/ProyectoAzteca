using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Clip : MonoBehaviour
{
    [Header("CONFIG")]
    public string bagTag;

    [Header("QUERY")]
    public BolsaDeUnKilo currentBagCode;

    public void openBag()
    {
        currentBagCode.OpenBag();
    }



    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(bagTag)){

            foreach (var hand in Player.instance.hands){
                hand.DetachObject(other.gameObject);
            }

            this.GetComponent<Rigidbody>().isKinematic = true;
            this.transform.parent = other.transform;

            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;

            currentBagCode = other.GetComponentInParent<BolsaDeUnKilo>();

            currentBagCode.CloseBag();
        }

    }

}
