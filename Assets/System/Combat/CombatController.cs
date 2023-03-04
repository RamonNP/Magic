using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private List<MagicStatus> _actionList;
    [SerializeField] private DamageText _damageTextPrefab;

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
        
        int damage = CalculateDamage(magicEnum.PlayerStatusController.Attributes.ATK, enemyStatus.Attribute.DEF);
        Debug.Log("ATK: " + magicEnum.PlayerStatusController.Attributes.ATK +" DEF"+ enemyStatus.Attribute.DEF);
        if (damage > 0)
        {
            ShowDamageText(damage, enemyStatus.transform);
            enemyStatus.transform.GetComponent<EnemyAnimationController>().PlayFXEnemy(magicEnum);
        } else
        {
            Debug.Log("IMPLEMENTAR SISTEMA DE BLOQUEIO DE ATAQUE");
        }
        Debug.Log("O dano causado foi: " + damage);


        //Play animation for damages
        //Calculate damage of enemy


        Debug.Log("CHGEOU NO FINAL IMPLEMNETAR COMBATE" + magicEnum.Magic +"  - Enemy = "+ enemyStatus.Enemy);
    }

    void ShowDamageText(int damageAmount, Transform transformdamage)
    {
        DamageText damageText = Instantiate(_damageTextPrefab, transformdamage.position, Quaternion.identity);
        damageText.transform.parent = transformdamage;
        damageText.ShowDamageText(damageAmount, transformdamage);
    }
    public int CalculateDamage(int attackValue, int defenseValue)
    {
        // Gerar um valor aleatório entre 80% e 100% do valor do ataque do jogador
        float variation = UnityEngine.Random.Range(0.8f, 1.0f);
        int damage = (int)(variation * attackValue);

        // Reduzir o dano com base na defesa do inimigo
        damage -= defenseValue;

        // Verificar se o dano é menor ou igual a zero
        if (damage <= 0)
        {
            // Se o dano for menor ou igual a zero, o inimigo bloqueou o ataque
            Debug.Log("O inimigo bloqueou o ataque!");
            damage = 0;
        }

        // Retornar o dano calculado
        return damage;
    }


}
