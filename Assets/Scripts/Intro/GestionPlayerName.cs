using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GestionPlayerName : MonoBehaviour
{
    public Animator animator; 

    public void closeFieldName()
    {
        animator.SetBool("PlayerNameIsOpen", false);
    }
}
