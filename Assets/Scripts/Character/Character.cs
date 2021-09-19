using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterAnimator animator;
    public float moveSpeed;
    public bool IsMoving { get; private set; }

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
    }

    public IEnumerator Move(Vector2 moveVec, Action onMoveOver = null)
    {
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f); //Bloque la valeur de moveVec.x entre -1 et 1
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f); //Bloque la valeur de moveVec.y entre -1 et 1

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        if (!IsWalkable(targetPos)) yield break; //si le npc ne peut pas marcher, on sort de la coroutine
        IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        IsMoving = false;

        //variable pour la rencontre dans les herbes
        onMoveOver?.Invoke(); //Appelle la méthode CheckForEncounter de la classe PlayerController quand le déplacement est terminé
    }

    public void HandleUpdate()
    {
        animator.IsMoving = IsMoving;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayer.Instance.SolidLayer | GameLayer.Instance.InteractableLayer) != null)
        {
            return false;
        }

        return true;
    }

    public CharacterAnimator Animator
    {
        get => animator;
    }

}
