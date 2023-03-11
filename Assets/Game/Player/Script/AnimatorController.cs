using Platformer.Magic.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private MagicCastController _magicController;
    [SerializeField] private AnimatorPlayerEvent _magicEvent;
    public Action<MagicEnum> OnplayerCastMagicByAnimation;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private AnimatorEnum _lastAnimation;
    private bool _isAttacking;

    private void Awake()
    {
        _playerController = transform.GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        _magicController.OnplayerCastMagicByBtn += CastMagicByEnum;
        _magicController.OnplayerChargind += PlayCharging;
        _magicEvent.OnplayerCastMagicByAnimation += MagicCastByAnimation;
        _playerController.OnplayerMove += MoveAndIdlePlay;
    }


    private void OnDisable()
    {
        _magicController.OnplayerChargind -= PlayCharging;
        _magicController.OnplayerCastMagicByBtn -= CastMagicByEnum;
        _magicEvent.OnplayerCastMagicByAnimation -= MagicCastByAnimation;
        _playerController.OnplayerMove -= MoveAndIdlePlay;
    }
    private void MoveAndIdlePlay(bool move)
    {
        if(move)
        {
            PlayAnimation(AnimatorEnum.Walking);
        } else
        {
            PlayAnimation(AnimatorEnum.Idle);
        }
    }

    private void PlayCharging(bool animation)
    {
        PlayAnimation(AnimatorEnum.ChargingPlayerAnimation);
    }    
    public void ShootArrow()
    {
        PlayAnimation(AnimatorEnum.Shooting);
    }    
    public void HighShot()
    {
        PlayAnimation(AnimatorEnum.HighShot);
    }
    
    private void PlayAnimation(AnimatorEnum animation)
    {
        if(AnimatorEnum.Shooting.Equals(animation) || AnimatorEnum.HighShot.Equals(animation))
        {
            StartCoroutine(PlayAndNotifyAnimationEnd(animation));

        } else if(!_isAttacking && !_lastAnimation.Equals(animation)) {
            _lastAnimation = animation;
            //Debug.Log("Animação Disparada - "+animation);
            _animator.Play(animation.ToString());
        }
    }

    public IEnumerator PlayAndNotifyAnimationEnd(AnimatorEnum animation)
    {
        _isAttacking = true;
        _animator.Play(animation.ToString());

        // Espera até que a animação termine
        while (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        // Avisar que a animação terminou
        //Debug.Log("A animação " + animation.ToString() + " terminou.");
        _isAttacking = false;
    }

    public void MagicCastByAnimation(MagicEnum magic)
    {
        switch (magic)
        {
            case MagicEnum.Fire:
                MagicControllerCastFireByAnimation();
                break;
            case MagicEnum.Fireball:

                break;
            case MagicEnum.MagicIce:

                break;
            case MagicEnum.Comet:

                break;
            case MagicEnum.Water:

                break;
            case MagicEnum.Tornado:

                break;
            case MagicEnum.Shield:
                break;
            case MagicEnum.Winter:
                break;
            default:
                break;
        }
    }

    public void MagicControllerCastFireByAnimation()
    {
        Debug.Log("Dispara Fire Iniciando aparição da Magia");
        _magicController.CastFire();
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fire);

    }    
    public void MagicControllerCastFireBallByAnimation()
    {
        Debug.Log("Dispara "+ MagicEnum.Fireball + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Fireball);
    }
    public void MagicControllerCometByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Comet + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Comet);
    }
    public void MagicControllerWaterByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Water + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Water);
    }
    public void MagicControllerMagicIceByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.MagicIce + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.MagicIce);
    }
    public void MagicControllerMagicTornadoByAnimation()
    {
        Debug.Log("Dispara " + MagicEnum.Tornado + " Iniciando aparição da Magia");
        OnplayerCastMagicByAnimation?.Invoke(MagicEnum.Tornado);
    }
    public void CastMagicByEnum(MagicEnum magic)
    {
        switch (magic)
        {
            case MagicEnum.Fire:
                Debug.Log("Dispara Fire Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.CastingSpells);    
                break;
            case MagicEnum.Fireball:
                Debug.Log("Dispara Fire Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.PlayerCastFireball);
                break;
            case MagicEnum.MagicIce:
                Debug.Log("Dispara Ice Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.CastIce);
                break;
            case MagicEnum.Comet:
                Debug.Log("Dispara " +magic+" Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.CastMagicComet);
                break;
            case MagicEnum.Water:
                Debug.Log("Dispara " + magic + " Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.CastWaterMagic);
                break;
            case MagicEnum.Tornado:
                Debug.Log("Dispara " + magic + " Iniciando Animação do PLayer");
                PlayAnimation(AnimatorEnum.CastTornadoMagic);
                break;
            case MagicEnum.Shield:
                break;
            case MagicEnum.Winter:
                break;
            default:
                break;
        }
    }
}

public enum AnimatorEnum
{
    Walking,
    ChargingPlayerAnimation,
    Idle,
    CastingSpells,
    PlayerCastFireball,
    CastIce,
    CastMagicComet,
    CastWaterMagic,
    CastTornadoMagic,
    Shooting,
    HighShot
}