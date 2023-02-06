using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private List<MagicStatus> _actionList;
    public Action<MagicStatus, EnemyStatus> OnPlayEnemyFX;

    private void OnEnable()
    {
        foreach (MagicStatus item in _actionList)
        {
            item.OnMagicalCollision += CombatCalculate;
        }
    }

    private void OnDisable()
    {
        foreach (MagicStatus item in _actionList)
        {
            item.OnMagicalCollision -= CombatCalculate;
        }
    }

    private void CombatCalculate(MagicStatus magicEnum, EnemyStatus enemyStatus)
    {


        //Play animation for damages
        enemyStatus.transform.GetComponent<EnemyAnimationController>().PlayFXEnemy(magicEnum);
        //Calculate damage of enemy


        Debug.Log("CHGEOU NO FINAL IMPLEMNETAR COMBATE" + magicEnum.Magic +"  - Enemy = "+ enemyStatus.Enemy);
    }
}
