using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// permet de puis le menu right click des asset de créer directemetn un nouveau pokemon dans unity
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new pokemon")]
// la public class est ScriptableObject !
public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;

    // la description des pokemons
    [TextArea]
    [SerializeField] string description;

    //les sprites des pokemons
    [SerializeField] Sprite frontsprite;
    [SerializeField] Sprite backsprite;

    //les types des pokemons
    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Base stats
    [SerializeField] int maxHP;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int spAttack;
    [SerializeField] int spDefense;
    [SerializeField] int speed;

    [SerializeField] List<LearnableMove> learnableMoves;
 
    //properties

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description;  }
    }

    public Sprite FrontSprite
    {
        get { return frontsprite; }
    }

    public Sprite BackSprite
    {
        get { return backsprite; }
    }

    public PokemonType Type1
    {
        get { return type1; }
    }

    public PokemonType Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return maxHP; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defense
    {
        get { return defense; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefense
    {
        get { return spDefense; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }
}

[System.Serializable]

//la ou on apprend les move
public class LearnableMove
{
    [SerializeField] MoveBase moveBase;
    [SerializeField] int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}
// ici les type des pokémons présent dans le jeu
public enum PokemonType
{
    None,
    Normal,
    Feu,
    Eau,
    Electrik,
    Plante,
    Glace,
    Combat,
    Poison,
    Sol,
    Vol,
    Psy,
    Insecte,
    Roche,
    Spectre,
    Dragon,
    Fée,
    Ténèbres,
    Acier,
    Inconnu,
}
