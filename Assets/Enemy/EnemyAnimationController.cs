using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FxIceEnemyDamage _fxIceEnemyController;
    [SerializeField] private FxEnemyController _fxFireEnemyController;


    public void PlayFXEnemy(MagicStatus magicEnum)
    {
        if(magicEnum.Magic.Equals(MagicEnum.MagicIce))
        {
            _animator.Play("HurtIce");
            _fxIceEnemyController.OnFxFinished += this.FinishAnimationFx;
            _fxIceEnemyController.FXDamageIcePlay();
        } else if (magicEnum.Magic.Equals(MagicEnum.Fire))
        {
            _animator.Play("Hurt");
            _fxFireEnemyController.OnFxFinished += this.FinishAnimationFx;
            _fxFireEnemyController.FXDamagePlay();
        }
    }

    public void FinishAnimationFx()
    {
        _animator.Play("Idle");
        _fxIceEnemyController.OnFxFinished -= this.FinishAnimationFx;
    }

}