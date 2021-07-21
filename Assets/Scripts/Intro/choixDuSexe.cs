using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class choixDuSexe : MonoBehaviour
{
    public Button btn;
    public Animator animator; 

    public void saveSexePlayer()
    {
        if (btn.name == "btnHomme") PlayerPrefs.SetString("sexePlayer", "Homme");
        else PlayerPrefs.SetString("sexePlayer", "Femme");

        closeWindowChoixSexe(); 
    }

    private void closeWindowChoixSexe()
    {
        animator.SetBool("ChoixSexeIsOpen", false);
    }
}
