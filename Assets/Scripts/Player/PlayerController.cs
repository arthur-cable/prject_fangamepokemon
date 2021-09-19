using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action OnEncountered; 

    private Vector2 input;

    private Character character;

    private void Awake()
    {
        character = GetComponent<Character>();
    }


    public void HandleUpdate()
    {
        if (!character.IsMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            //remove diagonal mouvement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, CheckForEncounters)); 
            }
        }

        character.HandleUpdate(); 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact(); 
        }
    }

    // pour le check des longues herbes
    private void CheckForEncounters()
    {
        //check si le player marchant dans les herbes rencontre un pokémon
        if (Physics2D.OverlapCircle(transform.position, 0.2f, GameLayer.Instance.GrassLayer) != null)
        {
          if  (UnityEngine.Random.Range(1, 101) <= 10)
            {
                character.Animator.IsMoving = false;
                OnEncountered(); 
            }
        }
    }

    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;
        //Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f); 

        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayer.Instance.InteractableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact(); 
        }
    }


}
