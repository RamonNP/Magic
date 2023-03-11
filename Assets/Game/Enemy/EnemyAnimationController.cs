using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FxIceEnemyDamage _fxIceEnemyController;
    [SerializeField] private FxEnemyController _fxFireEnemyController;
    [SerializeField] private EnemyStatus _enemyStatus;

    private void Start()
    {
        _enemyStatus = GetComponent<EnemyStatus>();    
    }
    public void PlayFXEnemy(MagicEnum magicEnum)
    {
        if(magicEnum.Equals(MagicEnum.MagicIce))
        {
            Play("HurtIce");
            _fxIceEnemyController.OnFxFinished += this.FinishAnimationFx;
            _fxIceEnemyController.FXDamageIcePlay();
        } else if (magicEnum.Equals(MagicEnum.Fire))
        {
            Play("Hurt");
            _fxFireEnemyController.OnFxFinished += this.FinishAnimationFx;
            _fxFireEnemyController.FXDamagePlay();
        } else
        {
            Play("Hurt");
        }
    }

    public void FinishAnimationFx()
    {
        Play("Idle");
        _fxIceEnemyController.OnFxFinished -= this.FinishAnimationFx;
    }

    public void PlayerAttackEnemy()
    {
        Play("Attacking");
    }    
    public void PlayerWalkkingEnemy()
    {
        Play("Walking");
    }
    public void PlayerEnemyDie(MagicEnum magicEnum)
    {
        Play("Dying");
    }

    private void Play(string Animation)
    {
        if(!_enemyStatus.IsDie || "Dying".Equals(Animation))
        {
            Debug.Log(Animation);
            _animator.Play(Animation);
        }
    }

}