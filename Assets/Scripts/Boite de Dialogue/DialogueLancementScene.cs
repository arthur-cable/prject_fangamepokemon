using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLancementScene: MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Dialogue dialogue;
    private Queue<string> sentences;


    private void Start()
    {
        //initialisation de la queue
        sentences = new Queue<string>();
        sentences.Clear();
        if (dialogue != null) //Si la classe dialogue est défini
        {
            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }
            ContinuerConversation(); //Engage le début de la conversation
        }
    }

    public void ContinuerConversation()
    {
        nameText.text = dialogue.name;
        if (sentences.Count == 0)
        {
            FinConversation();
            return;
        }
        dialogueText.text = sentences.Dequeue();
    }


    private void FinConversation()
    {
        Debug.Log("Fin de la conversation");
    }
}
