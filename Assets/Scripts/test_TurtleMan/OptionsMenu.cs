using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    Resolution[] resolutions;
    public Dropdown resolutionDropdown; 

    void Start()
    {
        // Définition de la résolution__________________________________________________________________________________________________________________
        List<string> listResolution = new List<string>();
        int currentResolutionIndex = 0;

        resolutions = Screen.resolutions;  // on liste les résolutions possibles pour l'écran utilisé
        resolutionDropdown.ClearOptions(); // on efface les options du dropdown

        for (int i = 0; i < resolutions.Length; i++) //on crée une liste composée de résolutions (sous format string) et on cherche quelle résolution est utilisée pour enregistrer l'indice correspondant
        {
            listResolution.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i; 
            }
        }
        
        resolutionDropdown.AddOptions(listResolution); //On affiche les différentes options possibles sur le dropdown
        resolutionDropdown.value = currentResolutionIndex; //La valeur affichée est celle positionnée à l'indice précédemment trouvé  
        resolutionDropdown.RefreshShownValue(); //On refresh
    }

    public void setResolution(int resolutionIndex)
    {
        resolutions = Screen.resolutions;   // on liste les résolutions possibles pour l'écran utilisé
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);  //On choisit la résolution selon l'indice sélectionné par le joueur dans le dropdown (sauf si option fullscreen cochée)
    }

    public void setFullScreen(bool isFullScreen)    //On affichera le jeu en plein écran si l'option "fullscreen" est cochée
    {
        Screen.fullScreen = isFullScreen; 
    }

    
}
