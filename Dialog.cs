using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //On définit la classe comme sérializable pour qu'on puisse l'utiliser dans d'autres script
public class Dialog
{
    public string name; //Nom du pnj
    [SerializeField] List<string> lines; 

    public List<string> Lines //Getter de l'attribut lines
    {
        get { return lines; }
    }
   
}
