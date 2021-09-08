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

    //ici on setup less degat infligés
    public bool TakeDamage(Move move, Pokemon attacker)
    {
        //formule permettant de calculé les dégats infligé
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    //Fonction qui choisit un move de l'adversaire
    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

}
