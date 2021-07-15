using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Toggle FullscreenToggle; 

    void Start()
    {
        // D�finition de la r�solution__________________________________________________________________________________________________________________
        List<string> listResolution = new List<string>();
        int currentResolutionIndex = 0;


        resolutions = Screen.resolutions;  // on liste les r�solutions possibles pour l'�cran utilis�
        resolutionDropdown.ClearOptions(); // on efface les options du dropdown

        for (int i = 0; i < resolutions.Length; i++) //on cr�e une liste compos�e de r�solutions (sous format string) et on cherche quelle r�solution est utilis�e pour enregistrer l'indice correspondant
        {
            listResolution.Add(resolutions[i].ToString());
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width && !(PlayerPrefs.HasKey("resolutionIndex")))
            {
                currentResolutionIndex = i; 
            }
        }
        
        resolutionDropdown.AddOptions(listResolution); //On affiche les diff�rentes options possibles sur le dropdown
        if (!PlayerPrefs.HasKey("resolutionIndex")) resolutionDropdown.value = currentResolutionIndex; //La valeur affich�e est celle positionn�e � l'indice pr�c�demment trouv�  
        else resolutionDropdown.value = PlayerPrefs.GetInt("resolutionIndex"); //La valeur affich�e est celle que le joueur � choisi pr�c�demment 
        resolutionDropdown.RefreshShownValue(); //On refresh


        //Gestion du toggleFullscreen_____________________________________________________________________________________________________________________
        FullscreenToggle.isOn = Screen.fullScreen; //Si le jeu est en plein �cran alors on coche automatiquement Toggle_fullscreen sur Unity
    }

    public void setResolution(int resolutionIndex)
    {
        resolutions = Screen.resolutions;   // on liste les r�solutions possibles pour l'�cran utilis�
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);  //On choisit la r�solution selon l'indice s�lectionn� par le joueur dans le dropdown (sauf si option fullscreen coch�e)
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex); //sauvegarde l'indice choisi de resolutionDropdown
    }

    public void setFullScreen(bool isFullScreen)    //On affichera le jeu en plein �cran si l'option "fullscreen" est coch�e
    {
        Screen.fullScreen = isFullScreen; 
    }

    
}
