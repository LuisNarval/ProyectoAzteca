using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class clockButton : MonoBehaviour
{
    [Header("CONFIG")]
    public float coldTime;

    [Header("Button Actions")]
    public UnityEvent Pressed;
    public UnityEvent UnPressed;

    [Header("QUERY")]
    public bool canBePressed = true;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (canBePressed){
                Pressed.Invoke();
                canBePressed = false;
                StartCoroutine(coroutine_ColdDown());
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player")){
            if (canBePressed){
                UnPressed.Invoke();
                canBePressed = false;
                StartCoroutine(coroutine_ColdDown());
            }
        }
    }




    IEnumerator coroutine_ColdDown()
    {
        yield return new WaitForSeconds(coldTime);
        canBePressed = true;
    }

}