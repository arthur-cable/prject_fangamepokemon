using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{ 
    [SerializeField] Button btnImput;
    private void Start()
    {
       // btnImput.name = Input.get"Interract"); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interract"))
        {
            Debug.Log(Input.inputString);
            Debug.Log("On a appuyé sur le btn interract"); 
        }

        
    }
}
