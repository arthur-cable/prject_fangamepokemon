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

    //Gestion du bouton continuer_______________________________________________________________________________________________________________________________
    //Rend visible ou non visible le bouton continuer selon les différents cas
    public void SetBtnContinuerVisible()
    {
        if (PlayerPrefs.HasKey("save")) btnContinuer.gameObject.SetActive(true); //S'il existe une save le bouton continuer apparaitra
        else btnContinuer.gameObject.SetActive(false);  //Sinon il n'apparaitra pas 
    }


    /*Dans le sceneManager, on a défini turtle_test (le menu) comme 1ere scene et scene_test comme seconde scene. En appuyant sur le bouton 
    "Continuer", on appelle la fonction Continuer(), ce qui nous permet de changer scene*/
    public void Continuer() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    //Gestion du bouton Nouvelle Partie___________________________________________________________________________________________________________________________4
    //En appuyant sur le bouton Nouvelle Partie, lance l'intro du professeur X (scene turtle_jeu) et mémorise la création d'une partie
    public void NouvellePartie()
    {
        SceneManager.LoadScene("turtle_jeu", LoadSceneMode.Single); //Charge la scene "turtle_jeu"
        PlayerPrefs.SetInt("save", 1); //Mémorise l'appuie sur le bouton Nouvelle Partie
    }



    //Gestion du bouton Continuer__________________________________________________________________________________________________________________________________
    /*En appuyant sur le bouton "Quitter", on quitte le jeu*/
    public void QuitGame() 
    {
        Debug.Log("Quit");
        PlayerPrefs.DeleteKey("save"); //ATTENTION : CETTE LIGNE SERT DE TEST MAIS DEVRA ETRE SUPPRIMER UN JOUR**********************************************************
        Application.Quit(); 
    }
}
