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
    [SerializeField] private GameObject _bloodSplashPrefab;
    [SerializeField] private float _yGap = 2.0f;

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

        EnemyHitEffects(enemy, magicEnum, damage, _arrowType.transform);
    }

    private void EnemyHitEffects(EnemyController enemy, MagicEnum magicEnum, int damage, Transform objectHitEnemy)
    {
        ShowDamageText(damage, enemy.transform);

        enemy.GetComponent<EnemyStatus>().SetIsDie(enemy.GetComponent<EnemyStatus>().Attribute.LoseLife(damage, magicEnum));
        enemy.GetComponent<EnemyStatus>().transform.GetComponent<EnemyAnimationController>().PlayFXEnemy(magicEnum);
        if (enemy.GetComponent<EnemyStatus>().IsDie)
        {
            enemy.GetComponent<EnemyStatus>().transform.GetComponent<EnemyAnimationController>().PlayerEnemyDie(magicEnum);
        }
        //desabilita o player para instancias  o KnockBack
        enemy.GetComponent<EnemyController>().DisableEnemy();
        ApplyKnockBack(enemy, objectHitEnemy);
        OnHitShake?.Invoke();
        SpawnBloodSplash(enemy.gameObject);
    }
    public void SpawnBloodSplash(GameObject location)
    {
        Vector3 spawnPos = new Vector3(location.transform.position.x, location.transform.position.y + _yGap, location.transform.position.z);
        // Instancia o prefab BloodSplash na posição e rotação do objeto "location"
        GameObject bloodSplash = Instantiate(_bloodSplashPrefab, spawnPos, location.transform.rotation);

        // Destroi o prefab BloodSplash depois de 2 segundos
        Destroy(bloodSplash, 2f);
    }

    private void ApplyKnockBack(EnemyController enemy, Transform objectHitEnemy)
    {
        float kx = 0;
        float enemyKnockbackX = enemy.GetComponent<Knockback>().KnockbackPosition.localPosition.x;

        if (objectHitEnemy.transform.position.x < enemy.transform.position.x)
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
        else if (objectHitEnemy.transform.position.x > enemy.transform.position.x)
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
        int damage = damage = CalculateDamage(magicEnum.PlayerStatusController.Attributes.AtkMagicPower, enemyStatus.Attribute.DEF);
        if (damage <= 0)
        {
            Debug.Log("IMPLEMENTAR SISTEMA DE BLOQUEIO DE ATAQUE");
            return;
        }

        EnemyHitEffects(enemyStatus.GetComponent<EnemyController>(), magicEnum.Magic, damage, magicEnum.transform);

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
