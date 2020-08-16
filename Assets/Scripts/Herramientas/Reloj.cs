using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloj : MonoBehaviour
{
    [Header("CONFIG")]
    public float menuDistance;
    public float menuAngle;
    public float menuHigh;

    public float inHeight;
    public float outHeight;


    [Header("REFERENCE")]
    [Header("UI")]
    public GameObject UIEnsambles;

    [Header("Reloj")]
    public GameObject button;
    public Material mat_In;
    public Material mat_Out;

    [Header("QUERY")]
    public bool Active;
    public GameObject vrCamera;

    private GameObject currentUI;

    // Start is called before the first frame update
    void Start()
    {
        Active = false;
        button.GetComponent<MeshRenderer>().material = mat_Out;
        button.transform.localPosition = new Vector3(0, outHeight, 0);
        vrCamera = Camera.main.gameObject;
    }

    public void TurnOnOff()
    {
        if (Active){
            Active = false;
            button.GetComponent<MeshRenderer>().material = mat_Out;
            button.transform.localPosition = new Vector3(0,outHeight,0);
            ocultarUI();
        }
        else{
            Active = true;
            button.GetComponent<MeshRenderer>().material = mat_In;
            button.transform.localPosition = new Vector3(0, inHeight, 0);
            mostrarUI();
        }
    }


    void mostrarUI()
    {

        Vector3 posUI = vrCamera.transform.position + 
            Vector3.Normalize(new Vector3(vrCamera.transform.forward.x,0.0f,vrCamera.transform.forward.z)) * menuDistance;
        

        posUI = new Vector3(posUI.x, vrCamera.transform.position.y*menuHigh, posUI.z);


        currentUI = Instantiate(UIEnsambles, posUI, 
                                Quaternion.Euler(menuAngle, vrCamera.transform.rotation.eulerAngles.y, 0.0f));


    }


    void ocultarUI()
    {
        Destroy(currentUI);
    }



}