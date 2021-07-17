using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player speed
    public float moveSpeed;

    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;

    //Player check 
    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            //remove diagonal mouvement
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;


                if (IsWalkable(targetPos))
                StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }
    //la coroutine
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

        //variable pour la rencontre dans les herbes
        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos)
    {
       if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }
    // pour le check des longues herbes
    private void CheckForEncounters()
    {
        //check si le player marchant dans les herbes rencontre un pok�mon
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grassLayer) != null)
        {
          if  (Random.Range(1, 101) <= 10)
            {
                // retour console comme quoi on rencontre bien le pok�mon
                Debug.Log("Encountered a wild pokemon");
            }
        }
    }


}
