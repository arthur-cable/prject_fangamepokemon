using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    public Dropdown vitesseDropdown;

    // Start is called before the first frame update
    void Start() 
    {
        if (!PlayerPrefs.HasKey("musicVolume")) //Si pas de préférence enregistrée, on enregistre par défault la valeur 1 
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            LoadVolume(); 
        }

        else //Si le joueur a déjà enregistré ses préférences pour le volume, alors on charge la valeur
        {
            LoadVolume(); 
        }
    }

   
    public void ChangeVolume() //change le volume selon la valeur du slider puis enregistre le volume choisi par le joueur
    {
        AudioListener.volume = volumeSlider.value;
        SaveVolume(); 
    }

    private void LoadVolume() //Charge le volume choisi par le joueur
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); 
    }

    private void SaveVolume() //Enregistre le volume choisi par le joueur
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); 
    }

}

