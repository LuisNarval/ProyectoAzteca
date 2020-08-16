using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ENSAMBLES{S, Z, ZA, ZB}
public class EnsambleMenu : MonoBehaviour
{
    [Header("REFERENCE")]
    public GameObject[] ensambles;
    public Image[] imgButtons;
    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void selectEnsamble(int index)
    {
        for(int i = 0; i<imgButtons.Length; i++){
            imgButtons[i].color = Color.white;
        }

        imgButtons[index].color = Color.green;

        instantiateHose(index);

    }


    void instantiateHose(int index)
    {
        Instantiate(ensambles[index], spawnPoint.transform.position, spawnPoint.transform.rotation);
    }


}