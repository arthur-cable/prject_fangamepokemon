using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text nameText; //Text sur Unity où s'affichera le nom du personnage qui parle
    public Text dialogueText; //Text sur Unity où s'affichera le dialogue
    public Animator animator;  //Gère l'animation de la boite de dialogue quand elle apparait et disparait
    private Queue<string> sentences; //donnée intermédaire qui stockera l'ensemble des phrases du dialogue
    private bool isTalking; //Pour savoir si la conversation est en cours


    private void Start()
    {
       sentences = new Queue<string>();  //initialisation de la queue
    }

    //permet de commencer la conversation
    public void CommencerConversation(Dialogue dialogue)
    {
        animator.SetBool("DialogueBoxIsOpen", true); //Déclenche l'ouverture de la boite de dialogue
        nameText.text = dialogue.name; //Permet l'affichage sur le jeu du nom de la personne qui parle
        isTalking = true;

        //initialisation de la queue
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
        if (sentences.Count == 0) //Quand la queue ne contient plus d'élément
        {
            FinConversation();
            return; //Permet de sortir de la fonction
        }
        string sentence = sentences.Dequeue(); //on stocke la phrase la plus haut de la pile dans la variable sentence
        sentence = CheckAbsenceDonneeSaved(sentence);
        StopAllCoroutines(); //On arrête toutes les autres coroutines du script (donc si le joueur veut passer à la prochaine phrase il pourra même si tout le texte n'est pas encore affiché)
        StartCoroutine(WriteSentence(sentence)); //lance la coroutine qui permet l'affichage progressif de sentence
    }

    private String CheckAbsenceDonneeSaved(string sentence)
    {
        if (sentence.Contains("PlayerPrefs.Get")) {
            bool trouve = false;
            int iDebut = sentence.IndexOf("PlayerPrefs.Get");
            int iFin = iDebut;

            while (trouve == false) {
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
           
            
            string str3 = sentence.Substring(iFin+1, sentence.Length - 1 - iFin);
            return CheckAbsenceDonneeSaved(String.Concat(str1, str2, str3)); 
        }
        else return sentence;
    }


    //Fonction qui permettra d'afficher progressivement le texte de sentence
    private IEnumerator WriteSentence(string sentence)
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
    void FinConversation()
    {
        isTalking = false;
        string cas = SceneManager.GetActiveScene().name;
        switch (cas)
        {
            case "turtle_jeu":
                if (PlayerPrefs.HasKey("PlayerName") == false)
                {
                    animator.SetBool("PlayerNameIsOpen", true);
                }
                else
                {
                    animator.SetBool("DialogueBoxIsOpen", false);
                    StartCoroutine(FermetureAvantChangementScene());   
                }
                break;


            default:
                animator.SetBool("DialogueBoxIsOpen", false);
                break;
        }
    }


    private IEnumerator FermetureAvantChangementScene()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene("scene_test", LoadSceneMode.Single); //Charge la scene scene_test

    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTalking==true) //Si on appuie sur la barre espace, permet de passer au dialogue suivante (il faudra peut être une condition plus restrictive quand on voudra parler au PNJ)
        {
            ContinuerConversation();
        }
    }
}
