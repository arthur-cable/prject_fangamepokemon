using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCControler : MonoBehaviour, Interactable 
{
    public enum NPCState { Idle, Walking, Dialog}


    [SerializeField] Dialog dialog;
    [SerializeField] List<Vector2> movementPattern;
    [SerializeField] float timeBetweenPattern;

    Character character;
    float idleTimer;
    int currentPattern = 0;
    NPCState state; 

    /*[SerializeField] List<Sprite> sprites;

SpriteAnimator spriteAnimator;*/


    private void Awake()
    {
        character = GetComponent<Character>();

    }


    /*private void Start()
    {
        spriteAnimator = new SpriteAnimator(sprites, GetComponent< SpriteRenderer >());
        spriteAnimator.Start(); 
    }

    private void Update()
    {
        spriteAnimator.HandleUpdate();
    }*/

    public void Interact()
    {
        if (state == NPCState.Idle) //si le pnj n'est pas a l arret, on ne peut pas parler => pas sur que condition intéressante 
        {
            state = NPCState.Dialog;
            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () => {
                idleTimer = 0f;
                state = NPCState.Idle; 
            })); 
        }
    }

    private void Update()
    {
        if (state == NPCState.Idle)
        {
            idleTimer += Time.deltaTime; 
            if (idleTimer > timeBetweenPattern)
            {
                idleTimer = 0f; 
                if (movementPattern.Count > 0) StartCoroutine(Walk()); 
            }
        }
        character.HandleUpdate();
    }

    IEnumerator Walk()
    {
        state = NPCState.Walking;

        var oldPosition = transform.position; 
        
        yield return character.Move(movementPattern[currentPattern]);

        if (transform.position != oldPosition) //si le character s'est déplacé, on passe au pattern suivant
            currentPattern = (currentPattern + 1) % movementPattern.Count;
        
        state = NPCState.Idle;
    }
}
