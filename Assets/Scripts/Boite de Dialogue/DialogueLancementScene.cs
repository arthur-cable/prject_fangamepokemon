using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* On priviligiera cette classe à l'ensemble TriggerDialogue et DialogueManager si on lance une conversation/dialogue au début d'une scene*/
public class DialogueLancementScene : MonoBehaviour
{
    public Text nameText;   //Text sur Unity où s'affichera le nom du personnage qui parle
    public Text dialogueText; //Text sur Unity où s'affichera le dialogue
    public Dialogue dialogue;   //class qui contient le dialogue (les phrases du dialogue et également le nom du perso qui parle)
    public Animator animator; //Gère l'animation de la boite de dialogue quand elle apparait et disparait
    private Queue<string> sentences; //donnée intermédaire qui stockera l'ensemble des phrases du dialogue

    //Puisqu'on lance la conversation dès le lancement de la scene, on utilise la méthode Start pour commencer le dialogue
    private void Start()
    {
        //Déclenche l'ouverture de la boite de dialogue
        animator.SetBool("isOpen", true);

        //initialisation de la queue
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence); //Permet de stocker toutes les phrases de dialogue dans la queue
        }
        ContinuerConversation(); //Engage le début de la conversation
    }

    //Méthode gèrant l'affichage du dialogue
    public void ContinuerConversation()
    {
        nameText.text = dialogue.name; //Permet l'affichage sur le jeu du nom de la personne qui parle
        if (sentences.Count == 0) //Quand la queue ne contient plus d'élément
        {
            FinConversation(); 
            return; //Permet de sortir de la fonction
        }
        string sentence = sentences.Dequeue(); //on stocke la phrase la plus haut de la pile dans la variable sentence
        StopAllCoroutines(); //On arrête toutes les autres coroutines du script (donc si le joueur veut passer à la prochaine phrase il pourra même si tout le texte n'est pas encore affiché)
        StartCoroutine(WriteSentence(sentence)); //lance la coroutine qui permet l'affichage progressif de sentence
    }

    //Fonction qui permettra d'afficher progressivement le texte de sentence
    IEnumerator WriteSentence(string sentence)
    {
        dialogueText.text = ""; //on vide la zone permettant l'affichage du dialogue sur unity 
        foreach (char letter in sentence.ToCharArray()) //On prend chaque caractere un par un de sentence
        {
            dialogueText.text += letter; //on ajoute au contenu déjà affiché la lettre suivante
            yield return null; //permet de skipper une frame

            /*Si on veut skipper plus de frame : 
            yield return new WaitForSeconds(0.01f); */
        }
    }

    //Fonction qui gère la fin d'une conversation 
    private void FinConversation()
    {
        animator.SetBool("isOpen", false); //Déclenche la fermeture de la boite de dialogue
        if (SceneManager.GetActiveScene().name == "turtle_jeu") //Dans le cas où la scene active est turtle_jeu
        {
            SceneManager.LoadScene("scene_test", LoadSceneMode.Single); //Charge la scene scene_test
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  //Si on appuie sur la barre espace
        {
            ContinuerConversation(); 
        }
    }
}
