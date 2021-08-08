using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;
    //check si le pokemon est un allié ou un adversaire
    [SerializeField] bool isPlayerUnit;

    public Pokemon Pokemon { get; set;  }


    public void Setup()
    {
      Pokemon =  new Pokemon(_base, level);
        //check d'un allié ou adversaire
        if (isPlayerUnit)
            // va chercher le sprite dans le dossier (attention au chemin d'accès !)
            GetComponent<Image>().sprite = Pokemon.Base.BackSprite;
        else
            GetComponent<Image>().sprite = Pokemon.Base.FrontSprite;
    }
}
