using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//états au début du combat
public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour

    //réference des scripts
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;
    
    public event Action<bool> OnBattleOver;
    
    BattleState state;
    int currentAction;
    int currentMove;



    // Start is called before the first frame update
    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);

        dialogBox.SetMoveNames(playerUnit.Pokemon.Moves);

        //si on met un $ devant un texte on peut allez chercher une valeur ou qqchose danns un code en C#
      yield return dialogBox.TypeDialog($"Un(e) {enemyUnit.Pokemon.Base.Name} sauvage apparait !");
      yield return new WaitForSeconds(1f);

        PlayerAction();

    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choisis une action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Pokemon.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} utilise {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        //check si l'ennmei meurt/est mort ou pas
      bool isFainted = enemyUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
       yield return enemyHud.UpdateHP();


        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} est mis KO !");
            //enemyUnit.PlayFaintAnimation(); a décommenter quand vidéo animation fight terminé 

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }

    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Pokemon.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.Pokemon.Base.Name} utilise {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        //check si l'ennmei meurt/est mort ou pas
        bool isFainted = playerUnit.Pokemon.TakeDamage(move, playerUnit.Pokemon);
      yield return  playerHud.UpdateHP();

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Pokemon.Base.Name} est mis KO !");
            //playerUnit.PlayFaintAnimation(); a décommenter quand vidéo animation fight terminé 
            
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);

        }
        else
        {
            PlayerAction();
        }
    }

    //selection d l'action possible via cette fonction
    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
          if (currentAction < 1)
            ++currentAction;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);
// choisis la selection par input
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                //fight
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                //run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 1)
                ++currentMove;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Pokemon.Moves.Count - 2)
                currentMove += 2;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove > 1)
                currentMove -= 2;
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Pokemon.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }


    /*ATTENTION : Mettre le code de la fonction Update dans le fonction HandleUpdate (ci-dessous)
    et décommenter le else if lié à battle system dans la fonction update du script GameControler
    => modif faite dans la vidéo 12 à 3min28 */
    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }
}