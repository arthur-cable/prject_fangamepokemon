using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogName;
    [SerializeField] Text dialogText;
    private float vitesseTexte;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public Animator animator;  //G�re l'animation de la boite de dialogue quand elle apparait et disparait

    private Dialog dialog;
    private int currentLine = 0;

    private bool continuer = false;
    private bool canTalk = true; 

    public bool IsShowing { get; private set; }

    public static DialogManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        vitesseTexte = PlayerPrefs.GetFloat("vitesseTexte");
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        if (canTalk)
        {
            yield return new WaitForEndOfFrame();
            OnShowDialog?.Invoke(); //Le ? correspond � l'op�rateur conditionnel nul => si OnShowDialog != null, la m�thode invoke est appel�e dans le cas inverse, Invoke n'est pas appel�e

            IsShowing = true;
            this.dialog = dialog;
            animator.SetBool("DialogBoxIsOpen", true); //D�clenche l'ouverture de la boite de dialogue
            dialogName.text = dialog.Name;
            string line = CheckAbsenceDonneeSaved(dialog.Lines[currentLine]); //permet si besoin de remplacer une partie de sentence par le contenu d'une donn�e sauvegard�e. 
            StartCoroutine(TypeDialog(line));
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) || continuer || Input.GetKey(KeyCode.LeftShift))
        {
            continuer = false;
            ++currentLine;
            if (currentLine < dialog.Lines.Count)
            {
                string line = CheckAbsenceDonneeSaved(dialog.Lines[currentLine]); //permet si besoin de remplacer une partie de sentence par le contenu d'une donn�e sauvegard�e. 
                StopAllCoroutines();    //permet de passer � la prochaine ligne sans attendre la fin de l'�criture
                StartCoroutine(TypeDialog(line)); //permet de passer � la prochaine ligne
            }
            else //Fin du dialogue
            {
                canTalk = false;
                currentLine = 0;
                StopAllCoroutines();    //permet d arr�ter le dialogue sans attendre la fin de l'�criture
                StartCoroutine(canTalkAgain());
                animator.SetBool("DialogBoxIsOpen", false); //D�clenche la fermeture de la boite de dialogue
                IsShowing = false; 

                OnCloseDialog?.Invoke(); //invoque l'event OnCloseDialogue
            }
        }
    }

    public void setContinuer()
    {
        continuer = true;
    }

    private IEnumerator canTalkAgain()
    {
        yield return new WaitForSeconds(0.60f);
        canTalk = true;
    }

    // M�thode qui permet d'afficher le contenu de donn�es sauvegard�es (comme le nom du joueur) en marquant dans le dialogue sur unity PlayerPrefs.Get("key"). Par exemple, sur unity, si on �crit 
    //dans le dialogue PlayerPrefs.Get("PlayerName") alors cette instruction sera remplac�e par le nom du joueur. Cette fonction marche �galemment avec les entiers et les chiffres � virgule.
    private String CheckAbsenceDonneeSaved(string line)
    {
        if (line.Contains("PlayerPrefs.Get"))
        {
            bool trouve = false;
            int iDebut = line.IndexOf("PlayerPrefs.Get");
            int iFin = iDebut;

            while (trouve == false)
            {
                if (line[iFin] == ')') trouve = true;
                else iFin++;
            }

            string str1 = line.Substring(0, iDebut);
            string str2 = null;

            switch (line[iDebut + 15])
            {
                case 'S':
                    string key = line.Substring(iDebut + 23, iFin - iDebut - 24);
                    str2 = PlayerPrefs.GetString(key);
                    break;

                case 'I':
                    key = line.Substring(iDebut + 20, iFin - iDebut - 21);
                    int keyInt = PlayerPrefs.GetInt(key);
                    str2 = keyInt.ToString();
                    break;

                case 'F':
                    key = line.Substring(iDebut + 22, iFin - iDebut - 23);
                    float keyFloat = PlayerPrefs.GetFloat(key);
                    str2 = keyFloat.ToString();
                    break;

                default:
                    break;
            }

            string str3 = line.Substring(iFin + 1, line.Length - 1 - iFin);
            return CheckAbsenceDonneeSaved(String.Concat(str1, str2, str3));
        }
        else return line;
    }


    //Fonction qui permettra d'afficher progressivement le texte de sentence
    private IEnumerator TypeDialog(string line)
    {
        dialogText.text = ""; //on vide la zone permettant l'affichage du dialogue sur unity 
        foreach (char letter in line.ToCharArray()) //On prend chaque caractere un par un de sentence
        {
            dialogText.text += letter; //on ajoute au contenu d�j� affich� la lettre suivante
            yield return new WaitForSeconds(vitesseTexte);
        }
    }

}
