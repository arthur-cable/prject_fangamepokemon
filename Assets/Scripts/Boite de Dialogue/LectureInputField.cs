using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class LectureInputField : MonoBehaviour
{
    public InputField imputField; 
    private string LireReponse()
    {
        return imputField.text; 
    }

    public void SauvegarderReponse()
    {
        string reponse = LireReponse();
        string cas = SceneManager.GetActiveScene().name;
        switch (cas)
        {
            case "turtle_jeu":
                PlayerPrefs.SetString("PlayerName", reponse);
                break;
            
            default:
                break;
        }
        //Debug.Log(PlayerPrefs.GetString("PlayerName")); Pour vérifier que ça fonctionne
    }
}
