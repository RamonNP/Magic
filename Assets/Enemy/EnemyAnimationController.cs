using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private FxIceEnemyDamage _fxEnemyController;


    public void PlayFXEnemy(MagicStatus magicEnum)
    {
        if(magicEnum.Magic.Equals(MagicEnum.MagicIce))
        {
            _animator.Play("HurtIce");
            _fxEnemyController.OnFxFinished += this.FinishAnimationFx;
            _fxEnemyController.FXDamageIcePlay();
        }
    }

    public void FinishAnimationFx()
    {
        _animator.Play("Idle");
        _fxEnemyController.OnFxFinished -= this.FinishAnimationFx;
    }

}