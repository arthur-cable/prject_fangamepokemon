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
        // D�finition de la r�solution__________________________________________________________________________________________________________________
        List<string> listResolution = new List<string>();
        int currentResolutionIndex = 0;

        resolutions = Screen.resolutions;  // on liste les r�solutions possibles pour l'�cran utilis�
        resolutionDropdown.ClearOptions(); // on efface les options du dropdown

        for (int i = 0; i < resolutions.Length; i++) //on cr�e une liste compos�e de r�solutions (sous format string) et on cherche quelle r�solution est utilis�e pour enregistrer l'indice correspondant
        {
            listResolution.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].height == Screen.currentResolution.height && resolutions[i].width == Screen.currentResolution.width)
            {
                currentResolutionIndex = i; 
            }
        }
        
        resolutionDropdown.AddOptions(listResolution); //On affiche les diff�rentes options possibles sur le dropdown
        resolutionDropdown.value = currentResolutionIndex; //La valeur affich�e est celle positionn�e � l'indice pr�c�demment trouv�  
        resolutionDropdown.RefreshShownValue(); //On refresh
    }

    public void setResolution(int resolutionIndex)
    {
        resolutions = Screen.resolutions;   // on liste les r�solutions possibles pour l'�cran utilis�
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);  //On choisit la r�solution selon l'indice s�lectionn� par le joueur dans le dropdown (sauf si option fullscreen coch�e)
    }

    public void setFullScreen(bool isFullScreen)    //On affichera le jeu en plein �cran si l'option "fullscreen" est coch�e
    {
        Screen.fullScreen = isFullScreen; 
    }

    
}
