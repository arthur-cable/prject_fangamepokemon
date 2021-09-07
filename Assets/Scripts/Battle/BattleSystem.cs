using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleHUD enemyHud;

    public event Action<bool> OnBattleOver;

    // Start is called before the first frame update
    public void Start()
    {
        SetupBattle();
    }

    public void SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Pokemon);
        enemyHud.SetData(enemyUnit.Pokemon);
    }


    /*ATTENTION : Mettre le code de la fonction Update dans le fonction HandleUpdate (ci-dessous)
    et d�commenter le else if li� � battle system dans la fonction update du script GameControler
    => modif faite dans la vid�o 12 � 3min28 */
    public void HandleUpdate()
    {

    }
}