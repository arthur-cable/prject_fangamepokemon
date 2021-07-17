using UnityEngine;

[System.Serializable] //On d�finit la classe comme s�rializable pour qu'on puisse l'utiliser dans d'autres script
public class Dialogue
{
    // Start is called before the first frame update
    public string name; //Nom du pnj

    [TextArea(3,10)] //met en forme la zone de texte dans l'inspecteur
    public string[] sentences; //phrases du dialogue
}
