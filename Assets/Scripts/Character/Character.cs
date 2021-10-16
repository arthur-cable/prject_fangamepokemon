using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterAnimator animator; 
    public float moveSpeed; //vitesse du perso
    public bool IsMoving { get; private set; } //mémoire pour savoir si le caractère se déplace

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    //Méthode permettant le déplacement d'un perso (player ou npc)
    public IEnumerator Move(Vector2 moveVec, Action onMoveOver = null)
    {
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f); //Bloque la valeur de moveVec.x entre -1 et 1
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f); //Bloque la valeur de moveVec.y entre -1 et 1

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!IsPathClear(targetPos)) yield break; //si le npc ne peut pas marcher, on sort de la coroutine
        
        IsMoving = true; 

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime); 
            yield return null;
        }
        transform.position = targetPos;

        IsMoving = false;

        //variable pour la rencontre dans les herbes
        onMoveOver?.Invoke(); //Appelle la méthode CheckForEncounter de la classe PlayerControler quand le déplacement est terminé
    }


    public void HandleUpdate() //Gestionnaire d'update appelé par npcControler ou playerControler
    {
        animator.IsMoving = IsMoving;
    }

    private bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;

        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayer.Instance.SolidLayer | GameLayer.Instance.InteractableLayer | GameLayer.Instance.PlayerLayer))
        {
            return false;
        }

        return true;
    }

    //Permet de savoir si la position où doit aller le character est une position où il a le droit d'aller 
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayer.Instance.SolidLayer | GameLayer.Instance.InteractableLayer) != null) //si objet appartenant au solid layer ou interactable layer dans un rayon de 0.2 par rapport à l'endroit
        {
            return false;
        }

        return true;
    }

    public void LookTowards(Vector3 targetPos)
    {
        var xdiff = Mathf.Floor(targetPos.x) - Mathf.Floor(transform.position.x);
        var ydiff = Mathf.Floor(targetPos.y) - Mathf.Floor(transform.position.y);

        if (xdiff==0 || ydiff == 0)
        {
            animator.MoveX = Mathf.Clamp(xdiff, -1f, 1f); //Bloque la valeur de xdiff entre -1 et 1
            animator.MoveY = Mathf.Clamp(ydiff, -1f, 1f); //Bloque la valeur de moveVec.y entre -1 et 1
        }

    }

    public CharacterAnimator Animator
    {
        get => animator;
    }

}
