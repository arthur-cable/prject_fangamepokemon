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
    private bool isTalking; //Pour savoir si la conversation est en cours
    private float vitesseTexte; 

    //Puisqu'on lance la conversation dès le lancement de la scene, on utilise la méthode Start pour commencer le dialogue
    private void Start()
    {
        vitesseTexte = PlayerPrefs.GetFloat("vitesseTexte"); 

        //Déclenche l'ouverture de la boite de dialogue et indique que la conversation est en cours.
        animator.SetBool("DialogueBoxIsOpen", true);
        isTalking = true; 

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
        sentence = CheckAbsenceDonneeSaved(sentence); //permet si besoin de remplacer une partie de sentence par le contenu d'une donnée sauvegardée.
        if (vitesseTexte != -1)
        {
            StopAllCoroutines(); //On arrête toutes les autres coroutines du script (donc si le joueur veut passer à la prochaine phrase il pourra même si tout le texte n'est pas encore affiché)
            StartCoroutine(WriteSentence(sentence)); //lance la coroutine qui permet l'affichage progressif de sentence
        }
        else dialogueText.text = sentence;
    }

    //Fonction qui permettra d'afficher progressivement le texte de sentence
    IEnumerator WriteSentence(string sentence)
    {
        dialogueText.text = ""; //on vide la zone permettant l'affichage du dialogue sur unity 
        foreach (char letter in sentence.ToCharArray()) //On prend chaque caractere un par un de sentence
        {
            dialogueText.text += letter; //on ajoute au contenu déjà affiché la lettre suivante
            yield return new WaitForSeconds(vitesseTexte); 
        }
    }

    // Méthode qui permet d'afficher le contenu de donnée sauvegardé (comme le nom du joueur) en marquant dans le dialogue sur unity PlayerPrefs.Get("key"). Par exemple, sur unity, si on écrit 
    //dans le dialogue PlayerPrefs.Get("PlayerName") alors cette instruction sera remplacée par le nom du joueur. Cette fonction marche égalemment avec les entiers et les chiffres à virgule. 
    private String CheckAbsenceDonneeSaved(string sentence)
    {
        if (sentence.Contains("PlayerPrefs.Get"))
        {
            bool trouve = false;
            int iDebut = sentence.IndexOf("PlayerPrefs.Get");
            int iFin = iDebut;

            while (trouve == false)
            {
                if (sentence[iFin] == ')') trouve = true;
                else iFin++;
            }

            string str1 = sentence.Substring(0, iDebut);
            string str2 = null;

            switch (sentence[iDebut + 15])
            {
                case 'S':
                    string key = sentence.Substring(iDebut + 23, iFin - iDebut - 24);
                    str2 = PlayerPrefs.GetString(key);
                    break;

                case 'I':
                    key = sentence.Substring(iDebut + 20, iFin - iDebut - 21);
                    int keyInt = PlayerPrefs.GetInt(key);
                    str2 = keyInt.ToString();
                    break;

                case 'F':
                    key = sentence.Substring(iDebut + 22, iFin - iDebut - 23);
                    float keyFloat = PlayerPrefs.GetFloat(key);
                    str2 = keyFloat.ToString();
                    break;

                default:
                    break;
            }


            string str3 = sentence.Substring(iFin + 1, sentence.Length - 1 - iFin);
            return CheckAbsenceDonneeSaved(String.Concat(str1, str2, str3));
        }
        else return sentence;
    }

    //Fonction qui gère la fin d'une conversation 
    private void FinConversation()
    {
        isTalking = false; 
        string cas = SceneManager.GetActiveScene().name; 
        switch(cas){ //Selon la scene active le comportement pour la fin de la conversation pourra varier.
            case "turtle_jeu": //Dans le cas où la scene active est turtle_jeu
                animator.SetBool("ChoixSexeIsOpen", true);
                break;
           
            default: //Cas par defaut => ferme la boite de dialogue
                animator.SetBool("isOpen", false); //Déclenche la fermeture de la boite de dialogue
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
