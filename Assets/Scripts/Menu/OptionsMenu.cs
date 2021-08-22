using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Toggle FullscreenToggle;
    public Dropdown vitesseTexteDropdown;

    void Start()
    {
        // Définition de la résolution__________________________________________________________________________________________________________________
        List<string> listResolution = new List<string>();
        int currentResolutionIndex = 0;


        resolutions = Screen.resolutions;  // on liste les résolutions possibles pour l'écran utilisé
        resolutionDropdown.ClearOptions(); // on efface les options du dropdown

        for (int i = 0; i < resolutions.Length; i++) //on crée une liste composée de résolutions (sous format string) et on cherche quelle résolution est utilisée pour enregistrer l'indice correspondant
        {
            listResolution.Add(resolutions[i].ToString());
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width && !(PlayerPrefs.HasKey("resolutionIndex")))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(listResolution); //On affiche les différentes options possibles sur le dropdown
        if (!PlayerPrefs.HasKey("resolutionIndex")) resolutionDropdown.value = currentResolutionIndex; //La valeur affichée est celle positionnée à l'indice précédemment trouvé  
        else resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex"); //La valeur affichée est celle que le joueur à choisi précédemment 
        resolutionDropdown.RefreshShownValue(); //On refresh


        //Gestion du toggleFullscreen_____________________________________________________________________________________________________________________
        FullscreenToggle.isOn = Screen.fullScreen; //Si le jeu est en plein écran alors on coche automatiquement Toggle_fullscreen sur Unity


        //Gestion du vitesseTexteDropdown_________________________________________________________________________________________________________________
        if (!PlayerPrefs.HasKey("vitesseTexte"))//Si le joueur n'a jamais choisi la vitesse du texte, on choisit par défaut la vitesse rapide.
        {
            vitesseTexteDropdown.value = 2; 
            PlayerPrefs.SetFloat("vitesseTexte", 0.0f); 
        }
        else vitesseTexteDropdown.value = loadIndexVitesseTexte();  //La valeur affichée est celle que le joueur à choisi précédemment 
        vitesseTexteDropdown.RefreshShownValue(); //On refresh
    }

    public void setResolution(int resolutionIndex)
    {
        resolutions = Screen.resolutions;   // on liste les résolutions possibles pour l'écran utilisé
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);  //On choisit la résolution selon l'indice sélectionné par le joueur dans le dropdown (sauf si option fullscreen cochée)
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex); //sauvegarde l'indice choisi de resolutionDropdown
    }

    public void setFullScreen(bool isFullScreen)    //On affichera le jeu en plein écran si l'option "fullscreen" est cochée
    {
        Screen.fullScreen = isFullScreen;
    }



    //Gestion de la vitesse du texte ____________________________________________________________________________________________________________________________________
    public void SaveVitesseTexte(int vitesseIndex) //Enregistre le volume choisi par le joueur
    {
        switch (vitesseIndex)
        {
            case 0: //Lent 
                PlayerPrefs.SetFloat("vitesseTexte", 0.04f);
                break;

            case 1: //Normal
                PlayerPrefs.SetFloat("vitesseTexte", 0.02f);
                break;

            case 2: //Rapide
                PlayerPrefs.SetFloat("vitesseTexte", 0.0f);
                break;
            
            case 3: //Instant
                PlayerPrefs.SetFloat("vitesseTexte", -1f);
                break; 
        }
    }

    private int loadIndexVitesseTexte()
    {
        int vitesseIndex; 
        float vitesseTexte = PlayerPrefs.GetFloat("vitesseTexte");

        switch (vitesseTexte)
        {
            case 0.04f: //Lent 
                vitesseIndex = 0; 
                break;

            case 0.02f: //Normal
                vitesseIndex = 1; 
                break;

            case 0.0f: //Rapide
                vitesseIndex = 2; 
                break;

            case -1f: //Instant
                vitesseIndex = 3; 
                break;
            
             default: 
                vitesseIndex = 2;
                break; 
        }
        return vitesseIndex; 
    }
}
