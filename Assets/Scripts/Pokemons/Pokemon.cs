using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// pas besoin de MonoBehaviour ici
public class Pokemon
{
    // class de c#
    public PokemonBase Base { get; set; }

    public int Level { get; set; }

    public int HP { get; set; }
    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        HP = MaxHp;


        //Generate Moves
        Moves = new List<Move>();

        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
                Moves.Add(new Move(move.Base));

            if (Moves.Count >= 4)
                break;
        }

    }

    public int Attack
    {
        //formule du calcul de l'attaque d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defense
    {
        //formule du calcul de la défense d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        //formule du calcul de l'attaque spéciale d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefense
    {
        //formule du calcul de la défense spéciale d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }

    public int Speed
    {
        //formule du calcul de la vitesse d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }

    public int MaxHp
    {
        //formule du calcul de la vie d'un pokemon actuellement
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

}
