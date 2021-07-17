using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    public Text nameText; //Text sur Unity o� s'affichera le nom du personnage qui parle
    public Text dialogueText; //Text sur Unity o� s'affichera le dialogue
    public Animator animator;  //G�re l'animation de la boite de dialogue quand elle apparait et disparait
    private Queue<string> sentences; //donn�e interm�daire qui stockera l'ensemble des phrases du dialogue


    private void Start()
    {
       sentences = new Queue<string>();  //initialisation de la queue
    }

    //permet de commencer
    public void CommencerConversation(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true); //D�clenche l'ouverture de la boite de dialogue
        nameText.text = dialogue.name; //Permet l'affichage sur le jeu du nom de la personne qui parle

        //initialisation de la queue
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //Permet de stocker toutes les phrases de dialogue dans la queue
        }
        ContinuerConversation(); //Engage le d�but de la conversation
    }

    //M�thode g�rant l'affichage du dialogue
    public void ContinuerConversation()
    {
        if (sentences.Count == 0) //Quand la queue ne contient plus d'�l�ment
        {
            FinConversation();
            return; //Permet de sortir de la fonction
        }
        string sentence = sentences.Dequeue(); //on stocke la phrase la plus haut de la pile dans la variable sentence
        StopAllCoroutines(); //On arr�te toutes les autres coroutines du script (donc si le joueur veut passer � la prochaine phrase il pourra m�me si tout le texte n'est pas encore affich�)
        StartCoroutine(WriteSentence(sentence)); //lance la coroutine qui permet l'affichage progressif de sentence
    }


    //Fonction qui permettra d'afficher progressivement le texte de sentence
    IEnumerator WriteSentence(string sentence)
    {
        dialogueText.text = ""; //on vide la zone permettant l'affichage du dialogue sur unity 
        foreach (char letter in sentence.ToCharArray()) //On prend chaque caractere un par un de sentence
        {
            dialogueText.text += letter; //on ajoute au contenu d�j� affich� la lettre suivante
            yield return null; //permet de skipper une frame

            /*Si on veut skipper plus de frame : 
            yield return new WaitForSeconds(0.01f); */
        }
    }

    //Fonction qui g�re la fin d'une conversation 
    void FinConversation()
    {
        animator.SetBool("isOpen", false); //D�clenche la fermeture de la boite de dialogue
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //Si on appuie sur la barre espace, permet de passer au dialogue suivante (il faudra peut �tre une condition plus restrictive quand on voudra parler au PNJ)
        {
            ContinuerConversation();
        }
    }
}
