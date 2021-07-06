using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    /*Dans sceneManager, on a défini turtle_test (le menu) comme 1ere scene et turtle_jeu comme seconde scene. En appuyant sur le bouton 
    nouvelle partie, on appelle la fonction PlayGame(), ce qui nous permet de changer Scene*/
    public void PlayGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*En appuyant sur le bouton "Quitter", on quitte le jeu*/
    public void QuitGame() 
    {
        Debug.Log("Quit"); 
        Application.Quit(); 
    }
}
