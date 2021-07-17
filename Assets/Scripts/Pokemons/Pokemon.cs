using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// pas besoin de MonoBehaviour ici
public class Pokemon
{
    // class de c#
    PokemonBase _base;
    int level;

    public Pokemon(PokemonBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
    }

    public int Attack
    {
        //formule du calcul de l'attaque d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }

    public int Defense
    {
        //formule du calcul de la défense d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.Defense * level) / 100f) + 5; }
    }

    public int SpAttack
    {
        //formule du calcul de l'attaque spéciale d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
    }

    public int SpDefense
    {
        //formule du calcul de la défense spéciale d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.SpDefense * level) / 100f) + 5; }
    }

    public int Speed
    {
        //formule du calcul de la vitesse d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    }

    public int MaxHp
    {
        //formule du calcul de la vie d'un pokemon actuellement
        get { return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 10; }
    }

}
