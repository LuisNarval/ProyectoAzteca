using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bascula : MonoBehaviour
{
    [Header("REFERENCE")]
    public Text txt_numbers;
    public Text txt_g;

    [Header("QUERY")]
    public bool TurnOn = false;
    public float currentWeight;
    public float weightOffset;

    // Start is called before the first frame update
    void Start()
    {
        TurnOn = false;
        currentWeight = 0.0f;
        txt_numbers.text = "";
        txt_g.text = "";
    }

    public void OnOff()
    {
        if (TurnOn)
        {
            TurnOn = false;
            txt_numbers.text = "";
            txt_g.text = "";
        }
        else{
            TurnOn = true;
            txt_numbers.text = currentWeight.ToString("F1");
            txt_g.text = "g";
            weightOffset = 0.0f;
        }
    }


    public void Tarar()
    {
        if (TurnOn)
        {
            weightOffset = currentWeight;
            actualizarDisplay();
        }
    }





   
    public void recieveCurrentWeight(float weight)
    {
        currentWeight = weight * 100;
        actualizarDisplay();
    }




    void actualizarDisplay()
    {
        if (TurnOn)
        {
            txt_numbers.text = (currentWeight - weightOffset).ToString("F1");
        }

    }


}