using UnityEngine;

[System.Serializable] //On définit la classe comme sérializable pour qu'on puisse l'utiliser dans d'autres script
public class Dialogue
{
    // Start is called before the first frame update
    public string name; //Nom du pnj

    [TextArea(3,10)] //met plus facilement en forme dans l'inspecteur la zone de texte
    public string[] sentences; //phrases du dialogue
}
