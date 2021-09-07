using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialog
{
    [SerializeField] string name;
    [TextArea(3, 10)] //met en forme la zone de texte dans l'inspecteur
    [SerializeField] List<string> lines;

    public string Name
    {
        get { return name; }
    }


    public List<string> Lines
    {
        get { return lines; } 
    }
}
