using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnsambleHUDButton : MonoBehaviour
{
    [Header("CONFIG")]
    public ENSAMBLES ensambleType;

    [Header("QUERY")]
    public EnsambleMenu codeMenu;


    private void Start()
    {
        codeMenu = this.gameObject.GetComponentInParent<EnsambleMenu>();
    }


    public void SendButton()
    {
        codeMenu.selectEnsamble((int)ensambleType);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendButton();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
       
        }
    }



}