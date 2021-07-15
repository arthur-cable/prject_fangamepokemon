using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;
    

    private void Start()
    {
       sentences = new Queue<string>();  //initialisation de la queue
    }


    public void CommencerConversation(Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); 
        }
        ContinuerConversation(); 
    }


    public void ContinuerConversation()
    {
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
