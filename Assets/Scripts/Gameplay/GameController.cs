using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Battle, Dialog, }

public class GameController : MonoBehaviour
{
     [SerializeField] PlayerController playerController;
     [SerializeField] BattleSystem battleSystem;
     [SerializeField] Camera worldCamera;

    GameState state;

    // Start is called before the first frame update
    void Start()
    {
        playerController.OnEncountered += StartBattle;
        DialogManager.Instance.OnShowDialog += () => state = GameState.Dialog;
        DialogManager.Instance.OnCloseDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    void StartBattle()
    {
        state = GameState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }

        /*else if (state == GameState.Battle)
        {
            BattleSystem.HandleUpdate(); 
        }*/

        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
    }
}
