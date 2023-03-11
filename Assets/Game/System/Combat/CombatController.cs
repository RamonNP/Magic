using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [SerializeField] private List<MagicStatus> _actionList;
    [SerializeField] private DamageText _damageTextPrefab;
    [SerializeField] private DistanceFightingController _distanceFightingController;

    public Action<MagicStatus, EnemyStatus> OnPlayEnemyFX;
    public Action OnHitShake;

    private void OnEnable()
    {
        _distanceFightingController.OnplayerShotArrowByAnimation += CombatArrowCalculate;
        foreach (MagicStatus item in _actionList)
        {
            item.OnMagicalCollision += CombatCalculate;
        }
    }

    
    private void CombatArrowCalculate(EnemyController enemy, Arrow _arrowType)
    {
        int damage = CalculateDamage(_distanceFightingController.PlayerStatusController.Attributes.AtkMagicPower, enemy.GetComponent<EnemyStatus>().Attribute.DEF);
        if (damage <= 0)
        {
            Debug.Log("IMPLEMENTAR SISTEMA DE BLOQUEIO DE ATAQUE");
            return;
        }

        ShowDamageText(damage, enemy.transform);

        MagicEnum magicEnum = default;
        switch (_arrowType.ArrowType)
        {
            case ArrowType.RegularArrow:
                break;
            case ArrowType.FireArrow:
                magicEnum = MagicEnum.Fire;
                break;
            case ArrowType.IceArrow:
                magicEnum = MagicEnum.MagicIce;
                break;
            case ArrowType.PoisonArrow:
                break;
            case ArrowType.ExplosiveArrow:
                break;
            case ArrowType.ElectricArrow:
                break;
            case ArrowType.LightArrow:
                break;
            case ArrowType.DarkArrow:
                break;
            case ArrowType.HomingArrow:
                break;
            case ArrowType.SplitArrow:
                break;
            default:
                break;
        }

        enemy.GetComponent<EnemyStatus>().SetIsDie(enemy.GetComponent<EnemyStatus>().Attribute.LoseLife(damage, magicEnum));
        enemy.GetComponent<EnemyStatus>().transform.GetComponent<EnemyAnimationController>().PlayFXEnemy(magicEnum);
        if (enemy.GetComponent<EnemyStatus>().IsDie)
        {
            enemy.GetComponent<EnemyStatus>().transform.GetComponent<EnemyAnimationController>().PlayerEnemyDie(magicEnum);
        }
        //desabilita o player para instancias  o KnockBack
        enemy.GetComponent<EnemyController>().DisableEnemy();
        ApplyKnockBack(enemy, _arrowType);
        OnHitShake?.Invoke();
    }

    private void ApplyKnockBack(EnemyController enemy, Arrow _arrowType)
    {
        float kx = 0;
        float enemyKnockbackX = enemy.GetComponent<Knockback>().KnockbackPosition.localPosition.x;

        if (_arrowType.transform.position.x < enemy.transform.position.x)
        {
            if (enemy.transform.GetComponent<EnemyController>().IsFacingRight && enemyKnockbackX > 0 || !enemy.transform.GetComponent<EnemyController>().IsFacingRight && enemyKnockbackX < 0)
            {
                kx = -enemyKnockbackX;
            }
            else
            {
                kx = enemyKnockbackX;
            }
            Debug.Log("Flecha a Esquerda");
        }
        else if (_arrowType.transform.position.x > enemy.transform.position.x)
        {
            if (enemy.transform.GetComponent<EnemyController>().IsFacingRight && enemyKnockbackX < 0 || !enemy.transform.GetComponent<EnemyController>().IsFacingRight && enemyKnockbackX > 0)
            {
                kx = -enemyKnockbackX;
            }
            else
            {
                kx = enemyKnockbackX;
            }
            Debug.Log("Flecha a Direita");
        }

        enemy.GetComponent<Knockback>().ApplyKnockback(kx);

    }

    private void OnDisable()
    {
        foreach (MagicStatus item in _actionList)
        {
            item.OnMagicalCollision -= CombatCalculate;
        }
    }

    private void CombatCalculate(MagicStatus magicEnum, EnemyStatus enemyStatus, TypeItem TypeItem)
    {
        int damage = 0;
        if (TypeItem.Equals(TypeItem.Staff))
        {
            damage = CalculateDamage(magicEnum.PlayerStatusController.Attributes.AtkMagicPower, enemyStatus.Attribute.DEF);
            Debug.Log("ATK: " + magicEnum.PlayerStatusController.Attributes.AtkMagicPower + " DEF"+ enemyStatus.Attribute.DEF);
        }
        if (damage > 0)
        {
            ShowDamageText(damage, enemyStatus.transform);
            enemyStatus.transform.GetComponent<EnemyAnimationController>().PlayFXEnemy(magicEnum.Magic);
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
        DamageText damageText = Instantiate(_damageTextPrefab, new Vector3(0,5,0), Quaternion.identity);
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
