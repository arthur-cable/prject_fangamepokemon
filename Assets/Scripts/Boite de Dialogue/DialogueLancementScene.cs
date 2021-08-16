using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* On priviligiera cette classe � l'ensemble TriggerDialogue et DialogueManager si on lance une conversation/dialogue au d�but d'une scene*/
public class DialogueLancementScene : MonoBehaviour
{
    public Text nameText;   //Text sur Unity o� s'affichera le nom du personnage qui parle
    public Text dialogueText; //Text sur Unity o� s'affichera le dialogue
    public Dialogue dialogue;   //class qui contient le dialogue (les phrases du dialogue et �galement le nom du perso qui parle)
    public Animator animator; //G�re l'animation de la boite de dialogue quand elle apparait et disparait

    private Queue<string> sentences; //donn�e interm�daire qui stockera l'ensemble des phrases du dialogue
    private bool isTalking; //Pour savoir si la conversation est en cours

    //Puisqu'on lance la conversation d�s le lancement de la scene, on utilise la m�thode Start pour commencer le dialogue
    private void Start()
    {
        PlayerPrefs.DeleteKey("PlayerName");//*******************************************************LIGNE DE TEST : A ENLEVER ABSOLUMENT APRES TEST *****************************
        PlayerPrefs.SetInt("Test", 7);//*************************************************************LIGNE DE TEST : A ENLEVER ABSOLUMENT APRES TEST *****************************

        //D�clenche l'ouverture de la boite de dialogue et indique que la conversation est en cours.
        animator.SetBool("DialogueBoxIsOpen", true);
        isTalking = true; 

        //initialisation de la queue
        sentences = new Queue<string>();
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
        nameText.text = dialogue.name; //Permet l'affichage sur le jeu du nom de la personne qui parle
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
    private void FinConversation()
    {
        isTalking = false; 
        string cas = SceneManager.GetActiveScene().name; 
        switch(cas){ //Selon la scene active le comportement pour la fin de la conversation pourra varier.
            case "turtle_jeu": //Dans le cas o� la scene active est turtle_jeu
                animator.SetBool("ChoixSexeIsOpen", true);
                break;
           
            default: //Cas par defaut => ferme la boite de dialogue
                animator.SetBool("isOpen", false); //D�clenche la fermeture de la boite de dialogue
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTalking == true)  //Si on appuie sur la barre espace
        {
            ContinuerConversation(); 
        }
    }
}
