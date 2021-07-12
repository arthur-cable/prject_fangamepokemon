using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class MainMenu : MonoBehaviour
{
    public Button btnContinuer;

    private void Start()
    {
        SetBtnContinuerVisible();
    }


    /*Dans sceneManager, on a défini turtle_test (le menu) comme 1ere scene et scene_test comme seconde scene. En appuyant sur le bouton 
    "Continuer", on appelle la fonction Continuer(), ce qui nous permet de changer Scene*/
    public void Continuer() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetBtnContinuerVisible()
    {
        if (PlayerPrefs.HasKey("save")) btnContinuer.gameObject.SetActive(true); //S'il existe une save le bouton continuer apparaitra
        else btnContinuer.gameObject.SetActive(false);  //Sinon il n'apparaitra pas 
    }

    public void NouvellePartie()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerPrefs.SetInt("save", 1); 
    }

    /*En appuyant sur le bouton "Quitter", on quitte le jeu*/
    public void QuitGame() 
    {
        Debug.Log("Quit");
        PlayerPrefs.DeleteKey("save"); //ATTENTION : CETTE LIGNE SERT DE TEST MAIS DEVRA ETRE SUPPRIMER UN JOUR
        Application.Quit(); 
    }
}
