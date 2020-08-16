using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class displayBotton : MonoBehaviour
{
    [Header("Button Actions")]
    public UnityEvent Pressed;
    public UnityEvent UnPressed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")){
            Pressed.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")){
            UnPressed.Invoke();
        }
    }

}